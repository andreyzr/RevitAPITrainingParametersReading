using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3._4AddingDiameterParamete
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var categorySet = new CategorySet();
            categorySet.Insert(Category.GetCategory(doc, BuiltInCategory.OST_PipeCurves));

            string diameter = null;



            IList<Pipe> pipes = new FilteredElementCollector(doc)
                .OfClass(typeof(Pipe))
                .Cast<Pipe>()
                .ToList();

            //using (Transaction ts = new Transaction(doc, "Add parameter"))
            //{
            //    ts.Start();
            //    CreateSharedParameter(uiapp.Application, doc, "Наименование", categorySet, BuiltInParameterGroup.PG_DATA, true);
            //    ts.Commit();
            //}

            using (Transaction ts = new Transaction(doc, "Set parameters"))
            {
                foreach (var pipe in pipes)
                {
                    ts.Start();
                    Parameter markParameter = pipe.LookupParameter("Наименование");
                    Parameter diameterPar1 = pipe.get_Parameter(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER);
                    Parameter diameterPar2 = pipe.get_Parameter(BuiltInParameter.RBS_PIPE_INNER_DIAM_PARAM);
                    //Parameter diameter2 = (UnitUtils.ConvertFromInternalUnits(duct.get_Parameter(BuiltInParameter.RBS_PIPE_INNER_DIAM_PARAM).AsDouble(), UnitTypeId.Millimeters));
                    double diameter1 = (UnitUtils.ConvertFromInternalUnits((diameterPar1).AsDouble(), UnitTypeId.Millimeters));
                    double diameter2 = (UnitUtils.ConvertFromInternalUnits((diameterPar2).AsDouble(), UnitTypeId.Millimeters));
                    diameter = $"Труба {diameter1:f0}/{diameter2:f0}";
                    markParameter.Set(diameter);
                    ts.Commit();
                }
            }
            return Result.Succeeded;
        }

        private static void CreateSharedParameter(Application application,
            Document doc, string parameterName, CategorySet categorySet,
            BuiltInParameterGroup builtInParameterGroup, bool isInstance)
        {
            DefinitionFile definitionFile = application.OpenSharedParameterFile();
            if (definitionFile == null)
            {
                TaskDialog.Show("Ошибка", "Не найден файл общих параметров");
                return;
            }

            Definition definitions = definitionFile.Groups
                .SelectMany(group => group.Definitions)
                .FirstOrDefault(def => def.Name.Equals(parameterName));
            if (definitions == null)
            {
                TaskDialog.Show("Ошибка", "Не найден указанный параметр");
                return;
            }

            Binding binding = application.Create.NewTypeBinding(categorySet);
            if (isInstance)
            {
                binding = application.Create.NewInstanceBinding(categorySet);
            }
            BindingMap map = doc.ParameterBindings;
            map.Insert(definitions, binding, builtInParameterGroup);
        }
    }
}
