using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsAnimation : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The material reference for the leg animation.", order = 1)]
	private Material LegMaterial;

	[SerializeField]
	[Tooltip("The time between the frames.", order = 2)]
	private float TimeBetweenFrames;

	[SerializeField]
	[Tooltip("The starting offset for the leg animation.", order = 3)]
	private int LegMaterialOffsetAnimationStartFrame;

	[SerializeField]
	[Tooltip("The ending offset for the leg animation.", order = 4)]
	private int LegMaterialOffsetAnimationStopFrame;

	private void Start()
	{
		if(LegMaterialOffsetAnimationStartFrame > LegMaterialOffsetAnimationStopFrame)
			throw new InvalidOperationException("The provided leg animation range is invalid.");

		if(LegMaterialOffsetAnimationStopFrame == LegMaterialOffsetAnimationStartFrame)
			throw new InvalidOperationException("The provided leg animation range is invalid.");
	}

	// Use this for initialization
	private void OnEnable()
	{
		StartCoroutine(LegAnimationRoutine());
	}

	private void OnDisable()
	{
		//Hard stop all the coroutines which will be animating the legs.
		StopAllCoroutines();
	}
	
	// Update is called once per frame
	private IEnumerator LegAnimationRoutine()
	{
		int currentOffset = LegMaterialOffsetAnimationStartFrame;

		while (true)
		{
			LegMaterial.mainTextureOffset = new Vector2(currentOffset / 10.0f, LegMaterial.mainTextureOffset.y);

			currentOffset++;

			//wrap around: https://stackoverflow.com/questions/14415753/wrap-value-into-range-min-max-without-division
			currentOffset = ((currentOffset - LegMaterialOffsetAnimationStartFrame) % (LegMaterialOffsetAnimationStopFrame - LegMaterialOffsetAnimationStartFrame)) + LegMaterialOffsetAnimationStartFrame;

			//TODO: Cache WaitForSeconds due to still terrible GC in Unity 2017.1
			yield return new WaitForSeconds(TimeBetweenFrames);
		}
	}
}
