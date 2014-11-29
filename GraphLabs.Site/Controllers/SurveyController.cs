﻿using GraphLabs.DomainModel;
using GraphLabs.DomainModel.Repositories;
using GraphLabs.Site.Controllers.Attributes;
using GraphLabs.Site.Controllers.LabWorks;
using GraphLabs.Site.Models;
using GraphLabs.Site.Utils;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GraphLabs.Site.Controllers
{
    [GLAuthorize(UserRole.Administrator, UserRole.Teacher)]
    public class SurveyController : GraphLabsController
    {

        public ActionResult Create ()
        {
            var emptyQuestion = new SurveyCreatingViewModel();
            emptyQuestion.Question = "Введите текст вопроса";
            emptyQuestion.QuestionOptions.Add(new KeyValuePair<string, bool>("Введите первый вариант ответа", true));
            return View(emptyQuestion);
        }

        [HttpPost]
        public ActionResult Create(string Question, Dictionary<string, bool> QuestionOptions)
        {
			var model = new SurveyCreatingViewModel(Question, QuestionOptions);
            if (model.IsValid)
            {
                model.Save();
            }
            return View(model);
        }

    }
}
