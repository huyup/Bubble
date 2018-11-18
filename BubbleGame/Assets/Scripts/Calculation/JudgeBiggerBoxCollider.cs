using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JudgeBiggerBoxCollider: MonoBehaviour {

    /// <summary>
    /// _boxAが_boxBより大きい場合、Trueを返す、
    /// _boxAが_boxBより小さい場合、Falseを返す、
    /// </summary>
    /// <param name="_boxA"></param>
    /// <param name="_boxB"></param>
    /// <returns></returns>
    public bool JudegeWhichBoxIsBigger(GameObject _boxA, GameObject _boxB)
    {
        //FIXME:ここのreturn文がおかしい
        if (_boxA == null || _boxB == null)
            return false;

        bool isBoxABiggerThanBoxB = false;

        bool isBoxALongerThanBoxB = false;
        bool isBoxAHigherThanBoxB = false;
        bool isBoxADeeperThanBoxB = false;

        isBoxALongerThanBoxB = JudgeTwoBoxColliderInLength(_boxA.GetComponent<SetBoxColliderVertice>().lengthVertices,
            _boxB.GetComponent<SetBoxColliderVertice>().lengthVertices);

        isBoxAHigherThanBoxB = JudgeTwoBoxColliderInHeight(_boxA.GetComponent<SetBoxColliderVertice>().heightVertices,
    _boxB.GetComponent<SetBoxColliderVertice>().heightVertices);

        isBoxADeeperThanBoxB = JudgeTwoBoxColliderInDepth(_boxA.GetComponent<SetBoxColliderVertice>().depthVertices,
    _boxB.GetComponent<SetBoxColliderVertice>().depthVertices);

        if(isBoxADeeperThanBoxB&&isBoxAHigherThanBoxB&&isBoxALongerThanBoxB)
        {
            isBoxABiggerThanBoxB = true;
        }
        else if(!isBoxADeeperThanBoxB&&!isBoxAHigherThanBoxB&&!isBoxALongerThanBoxB)
        {
            isBoxABiggerThanBoxB = false;
        }

        return isBoxABiggerThanBoxB;
    }

    private bool JudgeTwoBoxColliderInLength(Vector3[] _boxLengthVertices, Vector3[] _bubbleLengthVertices)
    {
        bool isBoxLoggerThanBubble = false;

        float boxLength = (_boxLengthVertices[0] - _boxLengthVertices[1]).magnitude;
        float bubbleLength = (_bubbleLengthVertices[0] - _bubbleLengthVertices[1]).magnitude;

        if(boxLength > bubbleLength)
        {
            isBoxLoggerThanBubble = true;
        }
        else
        {
            isBoxLoggerThanBubble = false;
        }

        return isBoxLoggerThanBubble;
    }

    private bool JudgeTwoBoxColliderInHeight(Vector3[] _boxHeightVertices, Vector3[] _bubbleHeightVertices)
    {
        bool isBoxHigherThanBubble = false;

        float boxHeight = (_boxHeightVertices[0] - _boxHeightVertices[1]).magnitude;
        float bubbleHeight = (_bubbleHeightVertices[0] - _bubbleHeightVertices[1]).magnitude;

        if (boxHeight > bubbleHeight)
        {
            isBoxHigherThanBubble = true;
        }
        else
        {
            isBoxHigherThanBubble = false;
        }

        return isBoxHigherThanBubble;
    }

    private bool JudgeTwoBoxColliderInDepth(Vector3[] _boxDepthVertices, Vector3[] _bubbleDepthVertices)
    {
        bool isBoxDeeperThanBubble = false;

        float boxDepth = (_boxDepthVertices[0] - _boxDepthVertices[1]).magnitude;
        float bubbleDepth = (_bubbleDepthVertices[0] - _bubbleDepthVertices[1]).magnitude;

        if (boxDepth > bubbleDepth)
        {
            isBoxDeeperThanBubble = true;
        }
        else
        {
            isBoxDeeperThanBubble = false;
        }

        return isBoxDeeperThanBubble;
    }
}
