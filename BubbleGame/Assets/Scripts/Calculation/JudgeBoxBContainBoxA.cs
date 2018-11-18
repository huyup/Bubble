using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeBoxBContainBoxA : MonoBehaviour
{
    /// <summary>
    /// 物体bの中に物体aがあるかどうかをチェック
    /// </summary>
    /// <param name="_boxA"></param>
    /// <param name="_boxB"></param>
    /// <returns></returns>
    public bool JudgeIsBoxBContainBoxA(GameObject _boxA, GameObject _boxB)
    {
        //FIXME:ここのreturn文がおかしい
        if (_boxA == null || _boxB == null)
            return false;

        bool isInsideInLength = false;
        bool isInsideInHeight = false;
        bool isInsideInDepth = false;

        isInsideInLength = JudgeIsInsideInLength(_boxA.GetComponent<SetBoxColliderVertice>().lengthVertices,
    _boxB.GetComponent<SetBoxColliderVertice>().lengthVertices);

        isInsideInHeight = JudgeIsInsideInHeight(_boxA.GetComponent<SetBoxColliderVertice>().heightVertices,
    _boxB.GetComponent<SetBoxColliderVertice>().heightVertices);

        isInsideInDepth = JudgeIsInsideInDepth(_boxA.GetComponent<SetBoxColliderVertice>().depthVertices,
    _boxB.GetComponent<SetBoxColliderVertice>().depthVertices);

        if (isInsideInLength && isInsideInHeight && isInsideInDepth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// [0]はmin,[1]はmax
    /// </summary>
    /// <param name="_boxLengthVertices"></param>
    /// <param name="_bubbleLengthVertices"></param>
    /// <returns></returns>
    private bool JudgeIsInsideInLength(Vector3[] _boxLengthVertices, Vector3[] _bubbleLengthVertices)
    {
        bool isVertice1Inside = false;
        bool isVertice2Inside = false;

        if (_boxLengthVertices[0].x >= _bubbleLengthVertices[0].x &&
            _boxLengthVertices[0].x <= _bubbleLengthVertices[1].x)
        {
            isVertice1Inside = true;
        }

        if (_boxLengthVertices[1].x >= _bubbleLengthVertices[0].x &&
            _boxLengthVertices[1].x <= _bubbleLengthVertices[1].x)
        {
            isVertice2Inside = true;
        }

        if (isVertice1Inside && isVertice2Inside)
        {

            return true;
        }
        else
            return false;

    }

    private bool JudgeIsInsideInHeight(Vector3[] _boxHeightVertices, Vector3[] _bubbleHeightVertices)
    {
        bool isVertice1Inside = false;
        bool isVertice2Inside = false;


        if (_boxHeightVertices[0].y >= _bubbleHeightVertices[0].y &&
            _boxHeightVertices[0].y <= _bubbleHeightVertices[1].y)
        {
            isVertice1Inside = true;
        }

        if (_boxHeightVertices[1].y >= _bubbleHeightVertices[0].y &&
            _boxHeightVertices[1].y <= _bubbleHeightVertices[1].y)
        {
            isVertice2Inside = true;
        }

        if (isVertice1Inside && isVertice2Inside)
        {
            return true;
        }
        else
            return false;

    }

    private bool JudgeIsInsideInDepth(Vector3[] _boxDepthVertices, Vector3[] _bubbleDepthVertices)
    {
        bool isVertice1Inside = false;
        bool isVertice2Inside = false;

        if (_boxDepthVertices[0].z >= _bubbleDepthVertices[0].z &&
            _boxDepthVertices[0].z <= _bubbleDepthVertices[1].z)
        {
            isVertice1Inside = true;
        }

        if (_boxDepthVertices[1].z >= _bubbleDepthVertices[0].z &&
            _boxDepthVertices[1].z <= _bubbleDepthVertices[1].z)
        {
            isVertice2Inside = true;
        }

        if (isVertice1Inside && isVertice2Inside)
        {
            return true;
        }
        else
            return false;

    }
}
