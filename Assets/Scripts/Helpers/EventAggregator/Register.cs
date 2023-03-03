namespace GlobalCommander
{
    public struct Register<T>
    {
        public T RegisterObject;
        public bool Add;

        public Register(T registerObject, bool add)
        {
            RegisterObject = registerObject;
            Add = add;
        }
    }
}