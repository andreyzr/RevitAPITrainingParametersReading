using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingParameterEntry
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

            if (selectedElemtnt is FamilyInstance)
            {
                using (Transaction ts=new Transaction(doc,"Set parameters" ))
                {
                    ts.Start();
                    var familyInstance = selectedElemtnt as FamilyInstance;
                    Parameter commentParameter = familyInstance.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
                    commentParameter.Set("Test Comment");

                    Parameter typeCommentsParameter = familyInstance.Symbol.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS);
                    typeCommentsParameter.Set("Test Comment");
                    ts.Commit();
                }
            }


            return Result.Succeeded;
        }
    }
}
