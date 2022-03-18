// Use InputBindingComposite<TValue> as a base class for a composite that returns
// values of type TValue.
// NOTE: It is possible to define a composite that returns different kinds of values
//       but doing so requires deriving directly from InputBindingComposite.
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEditor;

#if UNITY_EDITOR


[InitializeOnLoad] // Automatically register in editor.
#endif
// Determine how GetBindingDisplayString() formats the composite by applying
// the  DisplayStringFormat attribute.
[DisplayStringFormat("{Modifier}+{Vector2}")]
public class Vector2Modifier : InputBindingComposite<Vector2>
{
    // Each part binding is represented as a field of type int and annotated with
    // InputControlAttribute. Setting "layout" restricts the controls that
    // are made available for picking in the UI.
    //
    // On creation, the int value is set to an integer identifier for the binding
    // part. This identifier can read values from InputBindingCompositeContext.
    // See ReadValue() below.

    [InputControl(layout = "Button")]
    public int Modifier;

    [InputControl(layout = "Button")]
    public int Up;

    [InputControl(layout = "Button")]
    public int Down;

    [InputControl(layout = "Button")]
    public int Right;

    [InputControl(layout = "Button")]
    public int Left;

    // Any public field that is not annotated with InputControlAttribute is considered
    // a parameter of the composite. This can be set graphically in the UI and also
    // in the data (e.g. "custom(floatParameter=2.0)").

    // This method computes the resulting input value of the composite based
    // on the input from its part bindings.
    public override Vector2 ReadValue(ref InputBindingCompositeContext context)
    {
        Vector2 value = new Vector2(-2, -2);
        if (context.ReadValueAsButton(Modifier))
        {
            float up = context.ReadValue<float>(Up);
            float down = context.ReadValue<float>(Down);
            float right = context.ReadValue<float>(Right);
            float left = context.ReadValue<float>(Left);

            value = new Vector2(right - left, up - down);
        }
        //... do some processing and return value
        return value;
    }

    // This method computes the current actuation of the binding as a whole.
    public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
    {
        float up = context.ReadValue<float>(Up);
        float down = context.ReadValue<float>(Down);
        float right = context.ReadValue<float>(Right);
        float left = context.ReadValue<float>(Left);

        Vector2 value = new Vector2(Mathf.Abs(left - right), Mathf.Abs(up - down));
        // Compute normalized [0..1] magnitude value for current actuation level.
        return value.magnitude;
    }

    static Vector2Modifier()
    {

        // Can give custom name or use default (type name with "Composite" clipped off).
        // Same composite can be registered multiple times with different names to introduce
        // aliases.
        //
        // NOTE: Registering from the static constructor using InitializeOnLoad and
        //       RuntimeInitializeOnLoadMethod is only one way. You can register the
        //       composite from wherever it works best for you. Note, however, that
        //       the registration has to take place before the composite is first used
        //       in a binding. Also, for the composite to show in the editor, it has
        //       to be registered from code that runs in edit mode.
        InputSystem.RegisterBindingComposite<Vector2Modifier>();
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init() { } // Trigger static constructor.
}