namespace Kitakun.TagDiary.Services
{
    using System;
    using System.Collections.Generic;

    using Kitakun.TagDiary.Core.Domain;
    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.ViewModels.Models.SpaceOwnerModels;

    public class MapperService : IMapperService
    {
        private static Dictionary<(Type from, Type to), Func<object, object>> _maps = new Dictionary<(Type from, Type to), Func<object, object>>
        {
            [(typeof(DiaryRecord), typeof(SpaceOwnerViewElementModel))] = (dRecordObj) => 
            {
                var dRecord = (DiaryRecord)dRecordObj;
                var result = new SpaceOwnerViewElementModel
                {
                    CreatedAt = dRecord.CreatedAt,
                    HasPassword = dRecord.ProtectedByPassword,
                    ShortDescriptionText = dRecord.ShortDescription,
                    UrlToken = dRecord.TokenUrl,
                    Tags = dRecord.Tags,
                    Privacy = dRecord.Privacy
                };
                return result;
            }
        };

        public T Map<F, T>(F src)
            where F : class
            where T : class
        {
            var fromType = typeof(F);
            var toType = typeof(T);

            return _maps[(fromType, toType)](src) as T;
        }
    }
}
