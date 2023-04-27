using HW_11;

CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
CancellationToken token = cancelTokenSource.Token;


var array = new int[] { 73, 57, 49, 99, 133, 20, 1 };

var sortFunction = new MergeSort();

Console.WriteLine($"Array before sorting:");
sortFunction.ShowArray(array);
Console.WriteLine();

Console.WriteLine($"Array after sorting:");
Task task = new Task(() =>
{

    sortFunction.SortArray(array, 0, array.Length - 1);
    if (token.IsCancellationRequested)
    {
        throw new TimeoutException();
    }

}, token);
try
{

    task.Start();
    cancelTokenSource.CancelAfter(100);
    task.Wait();
}
catch (Exception)
{
    Console.WriteLine("\nTasks cancelled: timed out.\n");
    return;
}
finally
{
    cancelTokenSource.Dispose();
}

sortFunction.ShowArray(array);
Console.WriteLine();