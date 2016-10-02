using System;
using Microsoft.AspNet.Scaffolding;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using StudentManagerScaffolder;
using StudentManagerScaffolder.UI;

namespace StudentManagerScaffolder
{
    public class CustomCodeGenerator : CodeGenerator
    {
        private readonly Model _templateModel;
        private readonly Dictionary<string, object> _parameters;

        /// <summary>
        /// Constructor for the custom code generator
        /// </summary>
        /// <param name="context">Context of the current code generation operation based on how scaffolder was invoked(such as selected project/folder) </param>
        /// <param name="information">Code generation information that is defined in the factory class.</param>
        public CustomCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
        {
            _templateModel = new Model("");
            _parameters = new Dictionary<string, object>() { { "Model", _templateModel } };

        }


        /// <summary>
        /// Any UI to be displayed after the scaffolder has been selected from the Add Scaffold dialog.
        /// Any validation on the input for values in the UI should be completed before returning from this method.
        /// </summary>
        /// <returns></returns>
        public override bool ShowUIAndValidate()
        {
            SelectModelWindow window = new SelectModelWindow(_templateModel);
            var showDialog = window.ShowDialog();

            return showDialog != null && showDialog.Value;
        }

        /// <summary>
        /// This method is executed after the ShowUIAndValidate method, and this is where the actual code generation should occur.
        /// In this example, we are generating a new file from t4 template based on the ModelType selected in our UI.
        /// </summary>
        public override void GenerateCode()
        {
            if (string.IsNullOrEmpty(_templateModel.Name) || !_templateModel.Scaffold)
            {
                return;
            }

            var projects = Context.ActiveProject.DTE.Solution.Projects;

            foreach (var project in projects.OfType<Project>())
            {
                if (project.Name.ToLower().EndsWith("common"))
                {
                    ScaffoldCommonLayer(project);
                }

                if (project.Name.ToLower().EndsWith("ui"))
                {
                    ScaffoldUiLayer(project);
                }

                if (project.Name.ToLower().EndsWith("data"))
                {
                    ScaffoldDataLayer(project);
                }

                if (project.Name.ToLower().EndsWith("api"))
                {

                }

                if (project.Name.ToLower().EndsWith("tests"))
                {

                }

                if (project.Name.ToLower().EndsWith("reports"))
                {

                }
            }
        }

        private void ScaffoldDataLayer(Project project)
        {
            try
            {
                var folderPath = Path.Combine("Repositories");

                var repositoryOutputPath = Path.Combine(folderPath, _templateModel.Name + "Repository");
                this.AddFileFromTemplate(project, repositoryOutputPath, "RepositoryTemplate", _parameters, skipIfExists: true);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }
        }

        private void ScaffoldCommonLayer(Project project)
        {
            var folderPath = Path.Combine(_templateModel.NamePlural);

            try
            {
                AddFolder(project, folderPath);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

            try
            {
                var iServiceOutputPath = Path.Combine(folderPath, "I" + _templateModel.Name + "Service");
                this.AddFileFromTemplate(project, iServiceOutputPath, "IServiceTemplate", _parameters, skipIfExists: true);

                var serviceOutputPath = Path.Combine(folderPath, _templateModel.Name + "Service");
                this.AddFileFromTemplate(project, serviceOutputPath, "ServiceTemplate", _parameters, skipIfExists: true);

                var iRepositoryOutputPath = Path.Combine(folderPath, "I" + _templateModel.Name + "Repository");
                this.AddFileFromTemplate(project, iRepositoryOutputPath, "IRepositoryTemplate", _parameters, skipIfExists: true);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

            try
            {
                var modelFolderPath = Path.Combine("Models");
                var dtoFolderPath = Path.Combine("Models", "Dtos");

                var modelOutputPath = Path.Combine(modelFolderPath, _templateModel.Name);
                this.AddFileFromTemplate(project, modelOutputPath, "ModelTemplate", _parameters, skipIfExists: true);

                var dtoOutputPath = Path.Combine(dtoFolderPath, _templateModel.Name + "Dto");
                this.AddFileFromTemplate(project, dtoOutputPath, "DtoTemplate", _parameters, skipIfExists: true);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

        }

        private void ScaffoldUiLayer(Project project)
        {
            var controllerfolderPath = Path.Combine("Controllers");

            try
            {
                AddFolder(project, controllerfolderPath);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

            try
            {
                var outputPath = Path.Combine(controllerfolderPath, _templateModel.Name + "Controller");
                this.AddFileFromTemplate(project, outputPath, "ControllerTemplate", _parameters, skipIfExists: true);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

            var folderPath = Path.Combine("ViewModels", "Builders");

            try
            {
                AddFolder(project, folderPath);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

            var viewModelsfolderPath = Path.Combine("ViewModels", _templateModel.NamePlural);

            try
            {
                AddFolder(project, viewModelsfolderPath);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

            try
            {
                var outputPath = Path.Combine(folderPath, _templateModel.Name + "ViewModelBuilder");
                this.AddFileFromTemplate(project, outputPath, "ViewModelBuilderTemplate", _parameters, skipIfExists: true);

                var viewModelOutputPath = Path.Combine(viewModelsfolderPath, _templateModel.Name + "IndexViewModel");
                this.AddFileFromTemplate(project, viewModelOutputPath, "IndexViewModelTemplate", _parameters, skipIfExists: true);
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }

        }

    }
}
