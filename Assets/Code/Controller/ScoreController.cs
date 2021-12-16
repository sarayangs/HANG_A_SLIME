using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Controller
{
    private readonly ScoreViewModel _viewModel;

    public ScoreController(ScoreViewModel viewModel)
    {
        _viewModel = viewModel;

    }
}
