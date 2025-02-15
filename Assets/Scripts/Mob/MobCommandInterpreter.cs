using System.Collections;
using UnityEditor;
using UnityEngine;

public struct MobCommandInterpreterState
{
    public int currentCommandIndex;
    public readonly MobCommand[] commands;

    public MobCommandInterpreterState(MobCommand[] commands)
    {
        currentCommandIndex = 0;
        this.commands = commands;
    }

    public bool IsDead => currentCommandIndex >= commands.Length || currentCommandIndex < 0;
    public MobCommand CurrentCommand => IsDead ? null : commands[currentCommandIndex];

    public void NextCommand() => currentCommandIndex++;
    public void Goto(int index) => currentCommandIndex = index;
}

public class MobCommandInterpreter : MonoBehaviour
{
    public bool flipMovementY = false;

    [SerializeReference]
    public MobCommand[] commands;

    public MobCommandInterpreterState state;


    void OnEnable()
    {
        StartCoroutine(ExecuteCommands());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void OnDrawGizmos()
    {
        // Only draw if selected
        // if (!Selection.Contains(gameObject)) return;

        // Visualize the command sequence
        Vector2 currentPosition = transform.position;
        foreach (var command in commands)
        {
            switch (command)
            {
                case MoveForMobCommand moveCommand:
                    var movement = moveCommand.movement;
                    if (flipMovementY)
                    {
                        movement.y = -movement.y;
                    }

                    Gizmos.color = Color.cyan;
                    Util2D.Gizmos.DrawArrow(currentPosition, currentPosition + movement);
                    currentPosition += movement;
                    break;
            }
        }
    }


    private IEnumerator ExecuteCommands()
    {
        state = new(commands);

        while (!state.IsDead)
        {
            var command = state.CurrentCommand;
            if (command is null)
            {
                Debug.LogWarning("Null command found in MobCommandInterpreter", this);
                continue;
            }

            state.NextCommand();

            Debug.Log($"Executing command: {command.GetSummary()}", this);
            yield return StartCoroutine(command.Execute(gameObject, this));
            Debug.Log($"Done command: {command.GetSummary()}", this);
        }

        enabled = false;
    }
}