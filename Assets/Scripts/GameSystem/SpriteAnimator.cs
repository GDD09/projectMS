using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct StateSpritesItem
{
    public string name;
    public Sprite[] sprites;
}

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    // States and their sprites (but we need those to appear in the inspector... sigh)
    public StateSpritesItem[] stateSprites;

    // Current state
    public string state = "idle";

    // Current frame
    public int frame = 0;

    // Time to wait before changing frame
    public float frameDelay = 0.1f;

    // Time since last frame change
    private float stateTime = 0;

    private Dictionary<string, Sprite[]> states = new();
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        foreach (var stateSprite in stateSprites)
        {
            if (states.ContainsKey(stateSprite.name))
            {
                Debug.LogError($"{gameObject.name}: State {stateSprite.name} already exists");
                continue;
            }

            states[stateSprite.name] = stateSprite.sprites;
        }
    }

    void Update()
    {
        stateTime += Time.deltaTime;

        if (stateTime >= frameDelay)
        {
            stateTime = 0;
            frame = (frame + 1) % states[state].Length;
            spriteRenderer.sprite = states[state][frame];
        }
    }

    public void SetState(string state)
    {
        if (states[state] == null)
        {
            Debug.LogError("State " + state + " not found");
            return;
        }

        if (this.state != state)
        {
            this.state = state;
            frame = 0;
            spriteRenderer.sprite = states[state][frame];
        }
    }
}
