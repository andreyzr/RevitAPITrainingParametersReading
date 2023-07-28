using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3._2Pipelength
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            double length = 0;

            IList<Reference> selectedElementRedList = uidoc.Selection.PickObjects(ObjectType.Element, "Выберете элементы");

            foreach (var selectedElemen in selectedElementRedList)
            {
                Element element = doc.GetElement(selectedElemen);
                length += UnitUtils.ConvertFromInternalUnits(element.LookupParameter("Длина").AsDouble(), UnitTypeId.Meters); 
            }
            TaskDialog.Show("Общая длина труб:", length.ToString());

            return Result.Succeeded;
        }
    }
}
