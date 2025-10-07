namespace SOLID_and_KISS_principles.Interfaces;

public interface ITaskManager
{
    void AssignTask(string taskName, string assignee);
    void CreateSubTask(string parentTask, string subTask);
    void ReviewTask(string taskName);
}

public interface IWorker
{
    void WorkOnTask(string taskName);
    void CompleteTask(string taskName);
    void ReportProgress(string taskName, int progressPercentage);
}

public interface INotificationService
{
    void SendTaskAssignment(string recipient, string taskName);
    void SendTaskCompletion(string recipient, string taskName);
}