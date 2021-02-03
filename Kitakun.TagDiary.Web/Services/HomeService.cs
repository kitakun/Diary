namespace Kitakun.TagDiary.Web.Services
{
    using System.Threading.Tasks;

    using Grpc.Core;

    public class HomeService : Home.HomeBase
    {
        public override Task<FetchHomePreviewRecordsResponse> FetchHomePreviewRecords(FetchHomePreviewRecordsRequest request, ServerCallContext context)
        {
            var firstRec = new HomePreveiwRecordItem()
            {
                Id = "1",
                Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(System.DateTime.Now),
                SpaceId = "1",
                Title = "First one!",
            };
            firstRec.Tags.AddRange(new[]
            {
                "hi",
                "hello",
                "#1"
            });

            var recs = new Google.Protobuf.Collections.RepeatedField<HomePreveiwRecordItem>
            {
                firstRec
            };

            var result = new FetchHomePreviewRecordsResponse();
            result.Records.AddRange(recs);

            return Task.FromResult(result);
        }
    }
}
