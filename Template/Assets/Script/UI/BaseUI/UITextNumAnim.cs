using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITextNumAnim : Text
{
    #region numAnim

    private int _StartNum;
    private int _EndNum;
    private float _AnimTime;

    private int _NumStep;
    private int _CurNum;

    public void SetNumAnim(int startNum, int endNum, float AnimTime)
    {
        _StartNum = startNum;
        _EndNum = endNum;
        _AnimTime = AnimTime;

        _NumStep = (int)((_EndNum - _StartNum) / _AnimTime);
    }

    private void Update()
    {
        _CurNum = (int)(_StartNum + _NumStep * Time.deltaTime);
        text = _CurNum.ToString();
    }

    #endregion
}
