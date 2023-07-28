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
                Parameter lengthParameter = selectedElemtnt.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                if (lengthParameter.StorageType == StorageType.Double)
                {
                    double lengthValue = UnitUtils.ConvertFromInternalUnits(lengthParameter.AsDouble(), UnitTypeId.Meters);
                    TaskDialog.Show("Lenght2", lengthValue.ToString());
                }

                //Parameter lengthParameter1 = selectedElemtnt.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                //if (lengthParameter1.StorageType == StorageType.Double)
                //{
                //    double lengthValue = UnitUtils.ConvertFromInternalUnits(lengthParameter1.AsDouble(), UnitTypeId.Meters);
                //    TaskDialog.Show("Lenght2", lengthValue.ToString());
                //}
            }

            return Result.Succeeded;
        }
    }
}
