using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingParametersReading
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedRef = uidoc.Selection.PickObject(ObjectType.Element, "Выберете элемент");
            var selectedElemtnt=doc.GetElement(selectedRef);

            if(selectedElemtnt is Wall)
            {
                Parameter lengthParameter1=selectedElemtnt.LookupParameter("Длина");
                if(lengthParameter1.StorageType== StorageType.Double) 
                {
                    TaskDialog.Show("Length1",lengthParameter1.AsDouble().ToString());
                }

                Parameter lengthParameter2 = selectedElemtnt.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                if(lengthParameter2.StorageType== StorageType.Double)
                {
                    TaskDialog.Show("Lenght2",lengthParameter2.AsDouble().ToString());  
                }
            }

            return Result.Succeeded;
        }
    }
}
