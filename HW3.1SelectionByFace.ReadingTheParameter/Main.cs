using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3._1SelectionByFace.ReadingTheParameter
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedRef = uidoc.Selection.PickObject(ObjectType.Face, "Выберете элемент");
            var selectedElemtnt = doc.GetElement(selectedRef);

            Parameter volumeParameter = selectedElemtnt.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);


            TaskDialog.Show("Объем стены", volumeParameter.AsDouble().ToString());

            return Result.Succeeded;
        }
    }
}
