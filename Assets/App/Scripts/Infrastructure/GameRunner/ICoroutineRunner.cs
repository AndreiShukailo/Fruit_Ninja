using System.Collections;
using UnityEngine;

namespace App.Scripts.Infrastructure.GameRunner
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}