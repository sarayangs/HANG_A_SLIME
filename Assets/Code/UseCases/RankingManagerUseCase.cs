public class RankingManagerUseCase : IRankingManager
{
    public void GetAllData()
    {
        var firebaseFirestoreService = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        
        var firebaseDatabseService = ServiceLocator.Instance.GetService<FirebaseDatabaseService>();
        firebaseDatabseService.GetScores();
    }

    public void ArrangeByScore()
    {
        throw new System.NotImplementedException();
    }

    public void Send()
    {
        throw new System.NotImplementedException();
    }
}