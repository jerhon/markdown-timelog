using Honlsoft.TimeLog.Domain;
using MediatR;

namespace Honlsoft.TimeLog.UseCases.Logs;

/// <summary>
/// Summarizes the time records for a given date.  If the Date is not included, it will use today's date to lookup the records.
/// </summary>
public class SummarizeTimeRecords : IRequest<SummarizeTimeRecordsResult> {
    public DateOnly? Date { get; set; }
}

public class SummarizeTimeRecordsResult {
    public TimeReport Report { get; set; }
}

public class SummarizeHandler : IRequestHandler<SummarizeTimeRecords, SummarizeTimeRecordsResult> {
    private readonly ITimeRecordRepository _repository;

    public SummarizeHandler(ITimeRecordRepository repository) {
        _repository = repository;

    }

    public async Task<SummarizeTimeRecordsResult> Handle(SummarizeTimeRecords request, CancellationToken cancellationToken) {

       var date = request?.Date ?? DateOnly.FromDateTime(DateTime.Now);
       var timeSheet = await _repository.LookupTimeSheetAsync(date);

       timeSheet.FillGaps();

       var summary = timeSheet.SummarizeByTask();

       return new SummarizeTimeRecordsResult() {
           Report = summary
       };
    }
}