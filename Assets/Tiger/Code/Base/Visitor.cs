namespace Tiger {
    public interface IVisitor
    {
        void Visit(object o);
    }
    
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }

    public interface IVisitableDataRequester<T> : IVisitable {
        T _data { get; set; }

        public void SetData(T data) {
            _data = data;
        }
    }
}