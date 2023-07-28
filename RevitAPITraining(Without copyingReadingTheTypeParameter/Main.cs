using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_Without_copyingReadingTheTypeParameter
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
            var selectedElemtnt = doc.GetElement(selectedRef);

            if(selectedElemtnt is FamilyInstance)
            {
                var familyInstance=selectedElemtnt as FamilyInstance;
                //Parameter widthParameter1=familyInstance.Symbol.LookupParameter("Ширина");
                //TaskDialog.Show("Width info", widthParameter1.AsDouble().ToString());

                Parameter widthParameter2 = familyInstance.Symbol.get_Parameter(BuiltInParameter.CASEWORK_WIDTH);
                TaskDialog.Show("Width info", widthParameter2.AsDouble().ToString());
            }

            return Result.Succeeded;
        }
    }
}
