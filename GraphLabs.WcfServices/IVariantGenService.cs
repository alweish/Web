﻿using System.ServiceModel;
using GraphLabs.WcfServices.Data;
using System.ServiceModel.Web;

namespace GraphLabs.WcfServices
{
    /// <summary> Сервис для генераторов вариантов </summary>
    [ServiceContract]
    public interface IVariantGenService
    {
        /// <summary> Получает вариант задания по Id </summary>
        /// <param name="id"> Id варианта</param>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "genvariant?id={id}"
            )]
        TaskVariantDto GetVariant(long id);

        /// <summary> Регистрирует завершение выполнения задания </summary>
        /// <param name="info"> Новый вариант </param>
        /// <param name="taskId"> Id задания, для которого вариант. Слегка избыточен, но пусть будет. </param>
        /// <param name="updateExistent"> Обновить существующую версию? </param>
        /// <returns> True, если успешно сохранили. False, если вариант с таким номером уже есть в этом задании. </returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "genvariant?id={taskId}&update={updateExistent}"
            )]
        void SaveVariant(TaskVariantDto info, long taskId, bool updateExistent = false);
    }
}
