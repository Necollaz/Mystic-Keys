using System;

[Serializable]
public class ColorKeyCount
{
    public BaseColor Color;
    public int KeyCount;
    public int Layer;
    private int _amount = 3;

    public void Validate()
    {
        if(KeyCount % _amount != 0)
        {
            KeyCount = (KeyCount / _amount) * _amount;
        }
    }
}