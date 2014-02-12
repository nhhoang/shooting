using UnityEngine;
using System.Collections;

public class ZDepthMapData {
	public ZDepthMap.Type type;
	public float zDepth;
	
	public ZDepthMapData(ZDepthMap.Type mType, float mZDepth) {
		type = mType;
		zDepth = mZDepth;
	}
}

public class ZDepthMap {
	public static float zDepthBetweenItem = 0.1f;
	
	public enum Type {
		FLOOR = 0,
		BACKGROUND_TREE,
		TILE_BLOCK,
		GRASS,
		HIGHLIGHT,
		UNIT_SHADOW,
		CHARACTERS,
		FIGHT_COLLIDER,
		FIGHT_BULLET,
		FIGHT_FLY_UNIT,
		MOVE_BUILDING
	}
	
	public static ZDepthMapData[] zMap = {
		new ZDepthMapData(Type.FLOOR, 0.0f),
		new ZDepthMapData(Type.BACKGROUND_TREE, 0.1f),		
		new ZDepthMapData(Type.TILE_BLOCK, 0.2f),
		new ZDepthMapData(Type.GRASS, 0.3f),
		new ZDepthMapData(Type.HIGHLIGHT, 0.4f),
		new ZDepthMapData(Type.UNIT_SHADOW, 0.5f),
		new ZDepthMapData(Type.CHARACTERS, 0.1f),		// This will add to tile's zdepth to make sure it above floor
		new ZDepthMapData(Type.FIGHT_COLLIDER, 0.0f),	// Collider zdepth for units and bullets
		new ZDepthMapData(Type.FIGHT_BULLET, 0.1f),	// Always on top of all buildings and units
		new ZDepthMapData(Type.FIGHT_FLY_UNIT, 0.2f),
		new ZDepthMapData(Type.MOVE_BUILDING, 200f)
	};
	
	public static float Get(ZDepthMap.Type aType) {
		return zMap[(int)aType].zDepth;
	}
}