public static class Values
{
    private static float gunRotation;
    private static float windResistance;
    private static float bulletDrag;
    private static bool valuesChanged;

    public static float Rotation
    {
        get 
        {
            return gunRotation;
        }
        set 
        {
            gunRotation = value;
        }
    }

    public static float Wind
    {
        get 
        {
            return windResistance;
        }
        set 
        {
            windResistance = value;
        }
    }
    
    public static float Drag
    {
        get 
        {
            return bulletDrag;
        }
        set 
        {
            bulletDrag = value;
        }
    }

    public static bool ValuesChanged
    {
        get
        {
            return valuesChanged;
        }
        set
        {
            valuesChanged = value;
        }
    }
}