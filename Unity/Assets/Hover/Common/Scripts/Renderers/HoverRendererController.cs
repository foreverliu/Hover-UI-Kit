using Hover.Common.Items;
using Hover.Common.Items.Managers;
using Hover.Common.Renderers.Helpers;
using Hover.Common.Utils;
using UnityEngine;

namespace Hover.Common.Renderers {

	/*================================================================================================*/
	[ExecuteInEditMode]
	[RequireComponent(typeof(HoverItemData))]
	[RequireComponent(typeof(HoverItemHighlightState))]
	[RequireComponent(typeof(HoverItemSelectionState))]
	public abstract class HoverRendererController : MonoBehaviour,
															ISettingsController, IProximityProvider {

		public ISettingsControllerMap Controllers { get; private set; }

		public bool ShowProximityDebugLines = true;
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected HoverRendererController() {
			Controllers = new SettingsControllerMap();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void Update() {
			if ( ShowProximityDebugLines && Application.isPlaying ) {
				DrawProximityDebugLines();
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public abstract Vector3 GetNearestWorldPosition(Vector3 pFromWorldPosition);
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected void DrawProximityDebugLines() {
			HoverItemHighlightState.Highlight? nearHigh = 
				GetComponent<HoverItemHighlightState>().NearestHighlight;

			if ( nearHigh == null ) {
				return;
			}

			Vector3 cursorPos = nearHigh.Value.Data.transform.position;
			Vector3 nearPos = nearHigh.Value.NearestWorldPos;
			float prog = nearHigh.Value.Progress;
			Color color = (prog >= 1 ? new Color(0.3f, 1, 0.4f, 1) : 
				(prog <= 0 ? new Color(1, 0, 0, 0.25f) : new Color(1, 1, 0, prog*0.5f+0.25f)));

			Debug.DrawLine(nearPos, cursorPos, color);
		}
		
	}

}
