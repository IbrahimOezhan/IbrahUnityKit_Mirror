using UnityEngine;

public class Setting_Float : Setting_Number<float>
{
    public override void Decrement()
    {
        currentValue -= increment;
    }

    public override void Increment()
    {
        currentValue += increment;
    }

    public override bool SetValue(float value)
    {
        bool condition = value >= min && value <= max;

        if (condition)
        {
            currentValue = value;
        }

        return condition;
    }
}
