using Honlsoft.TimeLog.Domain;
using MediatR;

namespace Honlsoft.TimeLog.UseCases.Logs;

public class GetTimeRecords : IRequest<GetTimeRecordsResult> {

    public DateOnly? Date { get; set; }
}

public class GetTimeRecordsResult {

    public TimeSheet TimeSheet { get; set; }
}


public class GetTimeRecordsHandler : IRequestHandler<GetTimeRecords, GetTimeRecordsResult> {

    private readonly ITimeRecordRepository _timeRecordsRepository;

    public GetTimeRecordsHandler(ITimeRecordRepository timeRecordsRepository) {
        _timeRecordsRepository = timeRecordsRepository;
    }


    public async Task<GetTimeRecordsResult> Handle(GetTimeRecords request, CancellationToken cancellationToken) {

        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var timeSheet = await _timeRecordsRepository.LookupTimeSheetAsync(date);

        return new GetTimeRecordsResult() { TimeSheet = timeSheet };
    }
}