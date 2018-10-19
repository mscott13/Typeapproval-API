using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
    public Class1()
    {
        //in setup define client and progress reporter
        var httpProgressHandler = new ProgressMessageHandler();
        httpProgressHandler.InnerHandler = new HttpClientHandler();
        var client = new HttpClient(httpProgressHandler) { BaseAddress = new Uri(Settings.Default.ServerUrl) };

        //register to use elsewhere in the application, note it is better to resuse for lifetime of application than create for every call
        Mvx.RegisterSingleton(client);
        Mvx.RegisterSingleton(httpProgressHandler);
    }

//Where we are going to use it
    protected readonly ProgressMessageHandler httpProgressHandler = Mvx.Resolve<ProgressMessageHandler>();
    private IProgress<UploadProgress> httpProgressReport;

    public async Task<bool> PostAndReportProgress(Object someObject, CancellationToken cancellationToken, IProgress<UploadProgress> progress)
    {
        var returnValue = false;
        httpProgressReport = progress;
        httpProgressHandler.HttpSendProgress += HttpProgressHandlerOnHttpSendProgress;
        //see other gist for creating a multipart upload object, omitted here
        var result = await httpClient.PostAsync("Upload/", someObject, cancellationToken);
        if (result.IsSuccessStatusCode)
        {
            returnValue = true;
        }
        httpProgressHandler.HttpSendProgress -= HttpProgressHandlerOnHttpSendProgress;
        return returnValue;
    }

    private void HttpProgressHandlerOnHttpSendProgress(object sender, HttpProgressEventArgs httpProgressEventArgs)
    {
        if (null != httpProgressReport)
        {
            var progress = new UploadProgress();
            progress.PercentComplete = httpProgressEventArgs.ProgressPercentage;
            progress.Status = string.Format("{0} MB uploaded of {1} MB", httpProgressEventArgs.BytesTransferred / 1048576, httpProgressEventArgs.TotalBytes / 1048576);
            httpProgressReport.Report(progress);
        }
    }
}


