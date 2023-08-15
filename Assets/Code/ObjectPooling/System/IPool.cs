using UnityEngine;

namespace SpaceGame.ObjectPooling
{
    public interface IPool<T>
    {
        T Pull();

        void Puch(T t);
    }
}
