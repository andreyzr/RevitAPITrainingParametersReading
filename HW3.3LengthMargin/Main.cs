using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3._3LengthMargin
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
            using(Transaction ts=new Transaction(doc,"Set parameters"))
            {
                foreach (var selectedElemen in selectedElementRedList)
                {                   
                    ts.Start();
                    Element element = doc.GetElement(selectedElemen);
                    Parameter commentParameter = element.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
                    length= (UnitUtils.ConvertFromInternalUnits(element.LookupParameter("Длина").AsDouble(), UnitTypeId.Millimeters)*1.1);
                    commentParameter.Set(length.ToString());
                    ts.Commit();
                }
            }


            return Result.Succeeded;
        }
    }
}
