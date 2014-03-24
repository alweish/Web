﻿using GraphLabs.DomainModel;
using GraphLabs.Site.Logic.LabsLogic;
using GraphLabs.Site.Models;
using GraphLabs.Site.Utils;
using Newtonsoft.Json;
using System.Web.Mvc;
using System;

namespace GraphLabs.Site.Controllers
{
    public class LabsController : Controller
    {
        private readonly GraphLabsContext _ctx = new GraphLabsContext();
        private LabsLogic logic = new LabsLogic();

        #region Отображение списка лабораторных работ
        public ActionResult Index()
        {
            this.AllowAnonymous(_ctx);
            return View(logic.GetLabWorks());
        }

        [HttpPost]
        public string GetLabInfo(int Id)
        {
            this.AllowAnonymous(_ctx);            
            var lab = logic.GetLabWorkById(Id);
            if (lab == null)
            {
                return JsonConvert.SerializeObject( new JSONResultLabInfo(1) );
            }
            return JsonConvert.SerializeObject(new JSONResultLabInfo(lab) );
        }

        [HttpPost]
        public string DeleteLab(int Id)
        {
            this.AllowAnonymous(_ctx);

            var lab = logic.GetLabWorkById(Id);
            if (lab == null)
            {
                return "1";
            }

            return "2";
        }
        #endregion        

        #region Создание и редактирование лабораторной работы
        public ActionResult Create(long id = 0)
        {
            this.AllowAnonymous(_ctx);
            return View( new CreateLabModel(id, logic.GetTasks()) );
        }
        
        [HttpPost]
        public string Create(string Name, string DateFrom, string DateTo, string JsonArr, long Id = 0)
        {
            this.AllowAnonymous(_ctx);
            if (logic.ExistedLabWorksCount(Name, Id) != 0)
            {
                return JsonConvert.SerializeObject( new JSONResultCreateLab(1, Name) );
            };

            LabWork lab = logic.CreateOrGetLabWorkDependingOnId(Id);
            if (lab == null)
            {
                return JsonConvert.SerializeObject( new JSONResultCreateLab(3, Name));
            }
            
            lab.Name = Name;
            lab.AcquaintanceFrom = logic.ParseDate(DateFrom);
            lab.AcquaintanceTill = logic.ParseDate(DateTo);

            if (IsExistingLab(Id))
            {
                lab = logic.DeleteEntries(lab);
            }

            logic.SaveLabWork(lab, IdNewLab(Id));
            logic.SaveLabEntries(lab, JsonConvert.DeserializeObject<int[]>(JsonArr));
            logic.DeleteTasksVariantsFromLabVariants(lab);

            int resultCode = 0;
            if (IsExistingLab(Id))
            {
                resultCode = 4;
            }
            return JsonConvert.SerializeObject(new JSONResultCreateLab(resultCode, lab.Name, lab.Id));
        }        

        /// <summary> Проверяет, что обрабатывается существующая лабораторная работа </summary>
        private bool IsExistingLab(long Id)
        {
            return (Id != 0);
        }

        /// <summary> Проверяет, что обрабатывается новая лабораторная работа </summary>
        private bool IdNewLab(long Id)
        {
            return (Id == 0);
        }

        //В id передается результат запроса
        [HttpPost]
        public string EditLab(long Id)
        {
            this.AllowAnonymous(_ctx);
            var lab = logic.GetLabWorkById(Id);
            if (lab == null)
            {
                return JsonConvert.SerializeObject( new CreateLabModel(1) );
            }
            return JsonConvert.SerializeObject( new CreateLabModel(0, lab) );
        }
        #endregion

        #region Создание и редактирование варианта лабораторной работы
        public ActionResult CreateVariant(long labId = 0, long varId = 0)
        {
            this.AllowAnonymous(_ctx);
            var lab = logic.GetLabWorkById(labId);
            if (lab == null)
            {
                return HttpNotFound();
            }            
            return View( new CreateLabVariantModel(lab, varId) );
        }
        
        [HttpPost]
        public string CreateVariant(int Id, string Number, string JsonArr, int variantId = 0)
        {
            const string SuccesfulCreating = "0";
            const string LabWorkNotFoundError = "1";
            const string NameCollisionError = "2";
            const string LabVariantNotFoundError = "3";
            const string SuccesfulUpdating = "4";
            const string TaskVariantNotFoundError = "5";
            const string UnknownSavingError = "6";
            this.AllowAnonymous(_ctx);

            LabWork lab = logic.GetLabWorkById(Id);
            if (lab == null)
            {
                return LabWorkNotFoundError;
            }

            if (logic.ExistedLabVariantsCount(Number, lab.Id, variantId) != 0)
            {
                return NameCollisionError;
            }

            LabVariant labVar = logic.CreateOrGetLabVariantDependingOnId(variantId);
            if (labVar == null)
            {
                return LabVariantNotFoundError;
            }

            labVar.LabWork = lab;
            labVar.Number = Number;
            if (IsNewLabVar(variantId))
            {
                labVar.Version = 1;
            }
            else
            {
                ++labVar.Version;
            }

            try
            {
                labVar.TaskVariants.Clear();
                labVar.TaskVariants = logic.MakeTaskVariantsList(JsonConvert.DeserializeObject<int[]>(JsonArr));
            }
            catch (Exception)
            {
                return TaskVariantNotFoundError;
            }

            try
            {
                logic.SaveLabVariant(labVar, IsNewLabVar(variantId));
            }
            catch (Exception)
            {
                return UnknownSavingError;
            }

            if (IsNewLabVar(variantId))
            {
                return SuccesfulCreating;
            }
            else
            {
                return SuccesfulUpdating;
            }
        }

        /// <summary> Проверяет, что обрабатывается новый вариант лабораторной работы </summary>
        private bool IsNewLabVar(long Id)
        {
            return (Id == 0);
        }

        //В id передается результат, в Name - номер варианта
        [HttpPost]
        public string EditVariant(long varId)
        {
            const int VariantNotFoundError = 1;
            const int SuccesfulWork = 0;
            this.AllowAnonymous(_ctx);
            var variant = logic.GetLabVariantById(varId);
            if (variant == null)
            {
                return JsonConvert.SerializeObject( new JSONResultEditVariant(VariantNotFoundError) );
            }
            return JsonConvert.SerializeObject(new JSONResultEditVariant(SuccesfulWork, variant) );
        }
        #endregion
    }
}