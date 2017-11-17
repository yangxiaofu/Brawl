using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using UnityEngine.Assertions;
using Panda;

namespace Game.CameraUI{
	public class CameraZooming : MonoBehaviour 
	{
		[SerializeField] float _outerBorder = 50f;
		[SerializeField] float _innerBorder = 150f;
		[SerializeField] float _zoomSpeed = 1f;
		[SerializeField] float _maxZoomIn = 28.9f;
		[SerializeField] float _maxZoomOut = 80f;
		Boundaries _outerBoundaries;
		Boundaries _innerBoundaries;
		List<Character> _characters = new List<Character>();
		Camera _camera;
		CameraBoundariesLogic _logic;
		void Start()
		{
			_logic = new CameraBoundariesLogic();
			_characters = FindObjectsOfType<Character>().ToList();
			_camera = GetComponent<Camera>();

			Assert.IsTrue(_innerBorder > _outerBorder, "The inner border must be greater than the outer border");

			Assert.IsTrue(_outerBorder <= Screen.width, "The outer border should be less than " + Screen.width);

			Assert.IsTrue(_outerBorder <= Screen.height, "The outer border shoudl be less than " + Screen.height);
		}

		[Task]
		void ResetBoundaries()
		{
			_outerBoundaries = new Boundaries(_outerBorder, Screen.width, Screen.height);
			_innerBoundaries = new Boundaries(_innerBorder, Screen.width, Screen.height);
			
			Task.current.Succeed();
		}

		[Task]
		bool ZoomOut()
		{
			if (_camera.fieldOfView >= _maxZoomOut)
				return false;

			_camera.fieldOfView += Time.deltaTime * _zoomSpeed;
			return true;
		}

		[Task]
		bool ZoomIn()
		{
			if (_camera.fieldOfView <= _maxZoomIn)
				return false;

			_camera.fieldOfView -= Time.deltaTime * _zoomSpeed;
			return true;
		}

		[Task]
		bool AllCharactersWithinOuterBoundaries()
		{
			return CharactersAreWithinBoundaries(_outerBoundaries);
		}

		[Task]
		bool AllCharactersWithinInnerBoundaries()
		{
			return CharactersAreWithinBoundaries(_innerBoundaries);
		}

		bool CharactersAreWithinBoundaries(Boundaries boundaries)
		{
			for(int i = 0; i < _characters.Count; i++)
			{
				if (_characters[i] == null) 
					continue;

				var screenPos = _camera.WorldToScreenPoint(_characters[i].transform.position);

				if (!_logic.PositionWithinBoundaries(screenPos, boundaries)) 
					return false;
			}

			return true;
		}
	}
}

