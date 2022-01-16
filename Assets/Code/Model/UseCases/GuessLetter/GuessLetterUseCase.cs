using UnityEngine;

public class GuessLetterUseCase : ILetterGuesser
{
    private readonly RestClientAdapter _restClientAdapter;
    private readonly TokenRepository _tokenRepository;
    private readonly ICheckResponse _checkResponseUseCase;

    public GuessLetterUseCase(RestClientAdapter restClientAdapter, TokenRepository tokenRepository, ICheckResponse checkResponseUseCase)
    {
        _restClientAdapter = restClientAdapter;
        _tokenRepository = tokenRepository;
        _checkResponseUseCase = checkResponseUseCase;
    }
    
    public async void GuessLetter(string letter)
    {
        var request = new GuessLetterRequest { letter = letter, token = _tokenRepository.GetToken() };
        var response = await
            _restClientAdapter
                .PutWithParametersOnUrl<GuessLetterRequest, GuessLetterResponse>
                (
                    EndPoints.GuessLetter,
                    request
                );
    
        _tokenRepository.SetToken(response.token);
        _checkResponseUseCase.CheckWord(response, letter);
      
    }
    
    
}