using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3._4AddingDiameterParameter
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var categorySet=new CategorySet();
            categorySet.Insert(Category.GetCategory(doc, BuiltInCategory.OST_PipeCurves));


            using (Transaction ts = new Transaction(doc, "Add parameter"))
            {
                ts.Start();
                NewMethod(uiapp, doc, categorySet);
                ts.Commit();
            }

            return Result.Succeeded;
        }

        private static void NewMethod(Application application, 
            Document doc,string parameterName ,CategorySet categorySet,
            BuiltInParameterGroup builtInParameterGroup,bool isInstance)
        {
            DefinitionFile definitionFile = application.OpenSharedParameterFile();
            if(definitionFile == null)
            {
                TaskDialog.Show("Ошибка", "Не найден файл общих параметров");
                return;
            }

            Definition definitions=definitionFile.Groups
                .SelectMany(group=>group.Definitions)
                .FirstOrDefault(def=>def.Name.Equals(parameterName));
            if(definitions== null)
            {
                TaskDialog.Show("Ошибка", "Не найден указанный параметр");
                return;
            }

            Definition definition=definitionFile.Groups
        }
    }
}
