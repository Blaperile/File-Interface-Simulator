using File_Interface_Simulator.Models;
using FIS.BL;
using FIS.BL.Domain.Setup;
using FIS.BL.Util.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace File_Interface_Simulator.Controllers
{
    public class SpecificationSetupController : Controller
    {
        private readonly ISpecificationSetupManager specSetupManager = new SpecificationSetupManager();

        [HttpGet]
        public ActionResult UploadFieldSpecification()
        {
            var fieldSpecificationViewModel = new FieldSpecificationViewModel();
            return View("UploadFieldSpecification", fieldSpecificationViewModel);
        }

        [HttpPost]
        public ActionResult UploadFieldSpecification(FieldSpecificationViewModel fieldspecificationViewModel) //save entered data
        {
            FieldSpecification fieldSpecification = specSetupManager.AddFieldSpecification(fieldspecificationViewModel.Name, fieldspecificationViewModel.Path, fieldspecificationViewModel.Version);
            if (fieldSpecification != null)
            {
                return RedirectToAction("FieldSpecificationOverview");
            }
            else
            ViewBag.error = "Name combined with version must be unique";
            return View(fieldspecificationViewModel);
        }

        [HttpGet]
        public ActionResult UploadFileSpecification()
        {
            FileSpecificationViewModel model = new FileSpecificationViewModel();
            IEnumerable<FieldSpecification> fieldSpecifications = specSetupManager.GetFieldSpecificatons();
            IList<String> fieldSpecificationStrings = new List<String>();
            foreach (FieldSpecification fieldSpec in fieldSpecifications)
            {
                fieldSpecificationStrings.Add(fieldSpec.Name + " - " + fieldSpec.Version);
            }
            model.FieldSpecifications = fieldSpecificationStrings;
            return View("UploadFileSpecification", model);
        }

        [HttpPost]
        public ActionResult UploadFileSpecification(FileSpecificationViewModel model)
        {
           try
            {
                specSetupManager.AddFileSpecification(model.Name, model.Path, model.IsInput, model.InDirectoryPath, model.ArchiveDirectoryPath, model.ErrorDirectoryPath, model.OutDirectoryPath, model.Version, model.FieldSpecification);
                return RedirectToAction("FileSpecificationOverview");
           } catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                //TODO: manier vinden om dit uit model te laten onthouden?
                IEnumerable<FieldSpecification> fieldSpecifications = specSetupManager.GetFieldSpecificatons();
                IList<String> fieldSpecificationStrings = new List<String>();
                foreach (FieldSpecification fieldSpec in fieldSpecifications)
                {
                    fieldSpecificationStrings.Add(fieldSpec.Name + " - " + fieldSpec.Version);
                }
                model.FieldSpecifications = fieldSpecificationStrings;

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult FileSpecificationOverview()
        {
            IList<FileSpecification> fileSpecifications = specSetupManager.GetFileSpecifications();
            IList<FileSpecificationOverviewDetailModel> fileSpecificationModels = new List<FileSpecificationOverviewDetailModel>();
            foreach (FileSpecification fileSpecification in fileSpecifications)
            {
                FileSpecificationOverviewDetailModel fileSpecificationModel = new FileSpecificationOverviewDetailModel()
                {
                    Id = fileSpecification.FileSpecificationId,
                    Name = fileSpecification.Name,
                    CreationDate = fileSpecification.UploadDate,
                    Path = fileSpecification.Path,
                    Version = fileSpecification.Version
                };
                
                if (fileSpecification.IsInput)
                {
                    fileSpecificationModel.InputOutput = "Input";
                } else
                {
                    fileSpecificationModel.InputOutput = "Output";
                }

                fileSpecificationModels.Add(fileSpecificationModel);
            }
            return View("FileSpecificationOverview", fileSpecificationModels);
        }

        [HttpGet]
        public ActionResult FieldSpecificationOverview()
        {
            IList<FieldSpecification> fieldSpecifications = specSetupManager.GetFieldSpecificatons();
            IList<FieldSpecificationOverviewDetailModel> fieldSpecificationModels = new List<FieldSpecificationOverviewDetailModel>();
            foreach (FieldSpecification fieldSpecification in fieldSpecifications)
            {
                FieldSpecificationOverviewDetailModel fileSpecificationModel = new FieldSpecificationOverviewDetailModel()
                {
                    Name = fieldSpecification.Name,
                    CreationDate = fieldSpecification.UploadDate,
                    Path = fieldSpecification.Path,
                    Version = fieldSpecification.Version
                };

                fieldSpecificationModels.Add(fileSpecificationModel);
            }
            return View("FieldSpecificationOverview", fieldSpecificationModels);
        }

        [HttpGet]
        public ActionResult FileSpecificationDetail(int id = 1)
        {

            FileSpecification fileSpecification = specSetupManager.GetFileSpecification(id);

            FileSpecificationDetailViewModel model = new FileSpecificationDetailViewModel()
            {
                Name = fileSpecification.Name,
                Version = fileSpecification.Version,
                CreationDate = fileSpecification.UploadDate,
                InputOutput = fileSpecification.IsInput ? "Input" : "Output",
                HeaderConditions = new List<HeaderConditionDetailViewModel>(),
                GroupConditions = new List<GroupConditionViewModel>(),
                FieldConditions = new List<FieldConditionViewModel>()
            };
            if (fileSpecification.IsInput)
            {
                model.InFolder = fileSpecification.Directories.Where(d => d.Name.Equals("in")).First().Location;
                model.ArchiveFolder = fileSpecification.Directories.Where(d => d.Name.Equals("archive")).First().Location;
                model.ErrorFolder = fileSpecification.Directories.Where(d => d.Name.Equals("error")).First().Location;
            }
            else
            {
                model.OutputFolder = fileSpecification.Directories.Where(d => d.Name.Equals("out")).First().Location;
            }
            foreach(HeaderCondition headerCondition in fileSpecification.HeaderConditions)
            {
                HeaderConditionDetailViewModel headerConditionModel = new HeaderConditionDetailViewModel()
                {
                    Code = headerCondition.HeaderFieldCode,
                    Content = headerCondition.Description,
                    Datatype = headerCondition.Datatype,
                    Size = headerCondition.Size.ToString()
                };
                model.HeaderConditions.Add(headerConditionModel);
            }

            foreach(GroupCondition groupCondition in fileSpecification.GroupConditions)
            {
                GroupConditionViewModel groupConditionModel = new GroupConditionViewModel()
                {
                    Code = groupCondition.Code,
                    Description = groupCondition.Code,
                    Range = groupCondition.MinimumAmountOfOccurences + "-" + groupCondition.MaximumAmountOfOccurences,
                    AmountFields = groupCondition.FileSpecFieldConditions.Count,
                    Level = groupCondition.Level
                };
                model.GroupConditions.Add(groupConditionModel);
                
                foreach(FileSpecFieldCondition fileSpecFieldCondition in groupCondition.FileSpecFieldConditions)
                {
                    FieldConditionViewModel fieldConditionModel = new FieldConditionViewModel()
                    {
                        Code = fileSpecFieldCondition.Code,
                        Name = fileSpecFieldCondition.Description,
                        Optional = fileSpecFieldCondition.IsOptional ? "O" : "M",
                        Values = fileSpecFieldCondition.FieldSpecFieldCondition.AllowedValues.Count,
                        Datatype = fileSpecFieldCondition.FieldSpecFieldCondition.Datatype,
                        Size = fileSpecFieldCondition.FieldSpecFieldCondition.Size,
                        Format = fileSpecFieldCondition.FieldSpecFieldCondition.Format,
                        Group = fileSpecFieldCondition.Group.Description,
                        Level = "L"+fileSpecFieldCondition.Level.ToString()
                    };
                    model.FieldConditions.Add(fieldConditionModel);
                }
            }

            return View("FileSpecificationDetail", model);
        }

        public HttpStatusCodeResult RemoveFileSpecificationRPC(int id)
        {
            FileSpecification fileSpecification = specSetupManager.RemoveFileSpecification(id);

            if (fileSpecification != null)
            {
                return new HttpStatusCodeResult(200, "Succes");
            }

            return new HttpStatusCodeResult(500, "This file specification cannot be deleted because there are message linked to it!");
        }

        [HttpGet]
        public ActionResult FileSpecificationGroupDetail(int groupConditionId = 1)
        {
            GroupConditionDetailViewModel model = new GroupConditionDetailViewModel()
            {
                FieldConditions = new List<FieldConditionViewModel>()
            };

            GroupCondition groupCondition = specSetupManager.GetGroupCondition(groupConditionId);

            model.GroupCondition = new GroupConditionViewModel()
            {
                Code = groupCondition.Code,
                Description = groupCondition.Description,
                Range = groupCondition.MinimumAmountOfOccurences + "-" + groupCondition.MaximumAmountOfOccurences,
                AmountFields = groupCondition.FileSpecFieldConditions.Count,
                Level = groupCondition.Level
            };

            foreach (FileSpecFieldCondition fileSpecFieldCondition in groupCondition.FileSpecFieldConditions)
            {
                FieldConditionViewModel fieldConditionModel = new FieldConditionViewModel()
                {
                    Code = fileSpecFieldCondition.Code,
                    Name = fileSpecFieldCondition.Description,
                    Optional = fileSpecFieldCondition.IsOptional ? "O" : "M",
                    Values = fileSpecFieldCondition.FieldSpecFieldCondition.AllowedValues.Count,
                    Datatype = fileSpecFieldCondition.FieldSpecFieldCondition.Datatype,
                    Size = fileSpecFieldCondition.FieldSpecFieldCondition.Size,
                    Format = !String.IsNullOrEmpty(fileSpecFieldCondition.FieldSpecFieldCondition.Format) ? fileSpecFieldCondition.FieldSpecFieldCondition.Format : "-",
                    Group = fileSpecFieldCondition.Group.Description,
                    Level = "L" + fileSpecFieldCondition.Level.ToString()
                };
                model.FieldConditions.Add(fieldConditionModel);
            }

            return View("FileSpecificationGroupDetail", model);
        }

        [HttpGet]
        public ActionResult FileSpecificationFieldDetail(int id = 1)
        {
            FileSpecFieldCondition fileSpecFieldCondition = specSetupManager.GetFileSpecFieldCondition(id);

            FieldConditionViewModel model = new FieldConditionViewModel()
            {
                Code = fileSpecFieldCondition.Code,
                Name = fileSpecFieldCondition.Description,
                Optional = fileSpecFieldCondition.IsOptional ? "Optional" : "Mandatory",
                Values = fileSpecFieldCondition.FieldSpecFieldCondition.AllowedValues.Count,
                Datatype = fileSpecFieldCondition.FieldSpecFieldCondition.Datatype,
                Size = fileSpecFieldCondition.FieldSpecFieldCondition.Size,
                Format = fileSpecFieldCondition.FieldSpecFieldCondition.Format,
                Level = "L" + fileSpecFieldCondition.Level.ToString()
            };

            return View("FileSpecificationFieldDetail", model);
        }
    }
}