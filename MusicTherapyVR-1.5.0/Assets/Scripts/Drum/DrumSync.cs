using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class DrumSync : RealtimeComponent<DrumSyncModel>
{
    private PlaySound _play;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _play = GetComponent<PlaySound>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void UpdateLoopPlay()
    {
        _play.play = model.play;
    }

    private void UpdateMeshRendererColor()
    {
        _meshRenderer.material.color = model.color;
    }

    protected override void OnRealtimeModelReplaced(DrumSyncModel previousModel, DrumSyncModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.playDidChange -= PlayDidChange;
            previousModel.colorDidChange -= ColorDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.play = _play.play;
            currentModel.color = _meshRenderer.material.color;


            UpdateLoopPlay();
            UpdateMeshRendererColor();

            currentModel.playDidChange += PlayDidChange;
            currentModel.colorDidChange += ColorDidChange;
        }
    }

    private void PlayDidChange(DrumSyncModel model, bool value)
    {
        UpdateLoopPlay();
    }

    private void ColorDidChange(DrumSyncModel model, Color value)
    {
        // Update the mesh renderer
        UpdateMeshRendererColor();
    }

    public void SetPlay(bool play)
    {
        model.play = play;
    }

    public void SetColor(Color color)
    {
        model.color = color;
    }
}
