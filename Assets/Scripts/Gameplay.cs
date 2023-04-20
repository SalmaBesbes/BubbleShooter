using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    public ParticleSystem PopParticules;

    ParticleSystem.MainModule mainParticuleSystemModule;


    private void Awake()
    {
        mainParticuleSystemModule = PopParticules.main;
        GameManager.Instance.RegisterForOnEventOccured((this, "ShooterBubbleReady"), () =>
        {
            GameManager.Instance.GetShooterBubble().onReadyToCheckForMerge.RegisterOnce(this, CheckMerge);
        });

    }

    //TODO: make it recrusive, once bubble neighbours are stored
    public void CheckMerge(Bubble current) {

        var bubbles = GameManager.Instance.GetCurrentBubbles();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(current.transform.position, current.transform.localScale.x);

        List<Bubble> Neighboors = new List<Bubble>();
        foreach (var collider in colliders)
        {
            var b = collider.GetComponent<Bubble>();

            if (b != null)
            {
                Neighboors.Add(b);
            }
        }

        var bubbleToMerge = Neighboors.First(x => x.GetNumber() == current.GetNumber());

        if (bubbleToMerge != null)
        {
            Vector3 pos = bubbleToMerge.transform.position;

            PopParticules.Stop();
            PopParticules.transform.position = pos;

            mainParticuleSystemModule.startColor = bubbleToMerge.GetColor();
            bubbles.Remove(bubbleToMerge);
            Destroy(bubbleToMerge.gameObject);

            PopParticules.Play();


            float k = Mathf.Log(current.GetNumber(), 2);

            float newValue = Mathf.Pow(2, k + 1);

            var preset = GameManager.Instance.config.BubbblePresets.Find(x => x.Number == newValue);

            current.SetNumber(newValue);
            current.SetColor(preset.Color);

            current.transform.position = pos;
            bubbles.Remove(current);


            GameManager.Instance.SetCurrentBubbles(bubbles);

        }


    }



}
