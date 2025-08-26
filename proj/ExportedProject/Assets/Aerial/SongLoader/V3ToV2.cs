using System;
using System.Linq;
using SimpleJSON;
using UnityEngine;

public static class V3ToV2
{
    public static JSONObject Geometry(JSONObject other)
    {
        if (other == null) return null;
        var obj = new JSONObject();
        if (other["type"] == "CUSTOM")
        {
            obj["_type"] = other["type"];
            obj["_mesh"] = Mesh(other["mesh"]?.AsObject);
            obj["_material"] = other["material"].IsString
                ? other["material"]
                : Material(other["material"]?.AsObject);
            obj["_collision"] = other["collision"];
        }
        else
        {
            obj["_type"] = other["type"];
            obj["_material"] = other["material"].IsString
                ? other["material"]
                : Material(other["material"]?.AsObject);
            obj["_collision"] = other["collision"];
        }
        return obj;
    }

    public static JSONObject Mesh(JSONObject other)
    {
        if (other == null) return null;
        var obj = new JSONObject { ["_vertices"] = other["vertices"] };
        if (other.HasKey("uv")) obj["_uv"] = other["uv"];
        if (other.HasKey("triangles")) obj["_triangles"] = other["triangles"];
        return obj;
    }

    public static JSONObject Material(JSONObject other)
    {
        if (other == null) return null;
        var obj = new JSONObject { ["_shader"] = other["shader"] };
        if (other.HasKey("shaderKeywords")) obj["_shaderKeywords"] = other["shaderKeywords"];
        if (other.HasKey("collision")) obj["_collision"] = other["collision"];
        if (other.HasKey("track")) obj["_track"] = other["track"];
        if (other.HasKey("color")) obj["_color"] = other["color"];
        return obj;
    }

    public static Vector3? RescaleVector3(Vector3? vec3)
    {
        if (!vec3.HasValue) return null;
        var v = vec3.Value;
        return new Vector3(v.x / 0.6f, v.y / 0.6f, v.z / 0.6f);
    }

    public static JSONNode CustomDataObject(JSONNode node)
    {
        if (node == null || !node.Children.Any()) return null;
        var n = node.Clone();
        if (n.HasKey("color")) n["_color"] = n.HasKey("_color") ? n["_color"] : n["color"];
        if (n.HasKey("coordinates")) n["_position"] = n.HasKey("_position") ? n["_position"] : n["coordinates"];
        if (n.HasKey("disableNoteGravity")) n["_disableNoteGravity"] = n.HasKey("_disableNoteGravity") ? n["_disableNoteGravity"] : n["disableNoteGravity"];
        if (n.HasKey("disableNoteLook")) n["_disableNoteLook"] = n.HasKey("_disableNoteLook") ? n["_disableNoteLook"] : n["disableNoteLook"];
        if (n.HasKey("flip")) n["_flip"] = n.HasKey("_flip") ? n["_flip"] : n["flip"];
        if (n.HasKey("localRotation")) n["_localRotation"] = n.HasKey("_localRotation") ? n["_localRotation"] : n["localRotation"];
        if (n.HasKey("noteJumpMovementSpeed")) n["_noteJumpMovementSpeed"] = n.HasKey("_noteJumpMovementSpeed") ? n["_noteJumpMovementSpeed"] : n["noteJumpMovementSpeed"];
        if (n.HasKey("noteJumpStartBeatOffset")) n["_noteJumpStartBeatOffset"] = n.HasKey("_noteJumpStartBeatOffset") ? n["_noteJumpStartBeatOffset"] : n["noteJumpStartBeatOffset"];
        if (n.HasKey("spawnEffect") && !n.HasKey("_disableSpawnEffect")) n["_disableSpawnEffect"] = !n["spawnEffect"];
        if (n.HasKey("size")) n["_scale"] = n.HasKey("_scale") ? n["_scale"] : n["size"];
        if (n.HasKey("track")) n["_track"] = n.HasKey("_track") ? n["_track"] : n["track"];
        if (n.HasKey("uninteractable") && !n.HasKey("_interactable")) n["_interactable"] = !n["uninteractable"];
        if (n.HasKey("worldRotation")) n["_rotation"] = n.HasKey("_rotation") ? n["_rotation"] : n["worldRotation"];

        if (n.HasKey("animation") && !n.HasKey("_animation"))
        {
            var obj = new JSONObject();
            if (n["animation"].HasKey("color")) obj["_color"] = n["animation"]["color"];
            if (n["animation"].HasKey("definitePosition")) obj["_definitePosition"] = n["animation"]["definitePosition"];
            if (n["animation"].HasKey("dissolve")) obj["_dissolve"] = n["animation"]["dissolve"];
            if (n["animation"].HasKey("dissolveArrow")) obj["_dissolveArrow"] = n["animation"]["dissolveArrow"];
            if (n["animation"].HasKey("interactable")) obj["_interactable"] = n["animation"]["interactable"];
            if (n["animation"].HasKey("localRotation")) obj["_localRotation"] = n["animation"]["localRotation"];
            if (n["animation"].HasKey("offsetPosition")) obj["_position"] = n["animation"]["offsetPosition"];
            if (n["animation"].HasKey("offsetRotation")) obj["_rotation"] = n["animation"]["offsetRotation"];
            if (n["animation"].HasKey("scale")) obj["_scale"] = n["animation"]["scale"];
            if (n["animation"].HasKey("time")) obj["_time"] = n["animation"]["time"];
            if (obj.Children.Any()) n["_animation"] = obj;
        }

        // Clean-up
        string[] keysToRemove = {
            "color", "coordinates", "disableNoteGravity", "disableNoteLook", "flip", "localRotation",
            "noteJumpMovementSpeed", "noteJumpStartBeatOffset", "spawnEffect", "size", "track", "uninteractable",
            "worldRotation", "animation"
        };
        foreach (var key in keysToRemove)
        {
            if (n.HasKey(key)) n.Remove(key);
        }

        return n;
    }

    public static JSONNode CustomDataEvent(JSONNode node)
    {
        if (node == null || !node.Children.Any()) return null;
        var n = node.Clone();

        if (n.HasKey("color")) n["_color"] = n.HasKey("_color") ? n["_color"] : n["color"];
        if (n.HasKey("lightID")) n["_lightID"] = n.HasKey("_lightID") ? n["_lightID"] : n["lightID"];
        if (n.HasKey("easing")) n["_easing"] = n.HasKey("_easing") ? n["_easing"] : n["easing"];
        if (n.HasKey("lerpType")) n["_lerpType"] = n.HasKey("_lerpType") ? n["_lerpType"] : n["lerpType"];
        if (n.HasKey("nameFilter")) n["_nameFilter"] = n.HasKey("_nameFilter") ? n["_nameFilter"] : n["nameFilter"];
        if (n.HasKey("rotation")) n["_rotation"] = n.HasKey("_rotation") ? n["_rotation"] : n["rotation"];
        if (n.HasKey("step")) n["_step"] = n.HasKey("_step") ? n["_step"] : n["step"];
        if (n.HasKey("prop")) n["_prop"] = n.HasKey("_prop") ? n["_prop"] : n["prop"];
        if (n.HasKey("speed")) n["_speed"] = n.HasKey("_speed") ? n["_speed"] : n["speed"];
        if (n.HasKey("direction")) n["_direction"] = n.HasKey("_direction") ? n["_direction"] : n["direction"];
        if (n.HasKey("lockRotation")) n["_lockPosition"] = n.HasKey("_lockPosition") ? n["_lockPosition"] : n["lockRotation"];

        string[] keysToRemove = {
            "color", "lightID", "easing", "lerpType", "nameFilter", "rotation",
            "step", "prop", "speed", "direction", "lockRotation"
        };
        foreach (var key in keysToRemove)
        {
            if (n.HasKey(key)) n.Remove(key);
        }

        return n;
    }
public static JSONObject ConvertFullBeatmap(JSONObject v3)
{
    if (v3 == null) return null;

    var v2 = new JSONObject
    {
        ["_version"] = "2.0.0"
    };

    var notes = new JSONArray();

    if (v3.HasKey("colorNotes"))
    {
        foreach (JSONNode note in v3["colorNotes"].AsArray)
        {
            var converted = ConvertNote(note.AsObject);
            if (converted != null)
                notes.Add(converted);
        }
    }

    // Add notes from burstSliders (safely)
    if (v3.HasKey("burstSliders"))
    {
        foreach (JSONNode burst in v3["burstSliders"].AsArray)
        {
            var burstObj = burst.AsObject;
            var firstNote = MakeSafeNote(burstObj["b"], burstObj["x"], burstObj["y"], burstObj["c"], burstObj["d"]);
            var secondNote = MakeSafeNote(burstObj["tb"], burstObj["tx"], burstObj["ty"], burstObj["sc"], burstObj["d"]);

            notes.Add(firstNote);
            notes.Add(secondNote);
        }
    }

    v2["_notes"] = notes;

    // Process Obstacles
    var obstacles = new JSONArray();
    if (v3.HasKey("obstacles"))
    {
        foreach (JSONNode w in v3["obstacles"].AsArray)
        {
            var o = w.AsObject;
            var obstacle = new JSONObject
            {
                ["_time"] = o["b"],
                ["_type"] = ClampInt(o["t"].AsInt, 0, 1), // crouch/wall
                ["_duration"] = o["d"],
                ["_lineIndex"] = ClampInt(o["x"].AsInt, 0, 3),
                ["_width"] = ClampInt(o["w"].AsInt, 1, 4),
                ["_height"] = o.HasKey("h") ? ClampInt(o["h"].AsInt, 1, 5) : 5
            };
            obstacles.Add(obstacle);
        }
    }
    v2["_obstacles"] = obstacles;

    // Process Events
    var events = new JSONArray();

    if (v3.HasKey("basicBeatmapEvents"))
    {
        foreach (JSONNode ev in v3["basicBeatmapEvents"].AsArray)
        {
            var type = ev["et"].IsNumber ? ev["et"] : ev["t"];
            var value = ClampInt(ev["v"].AsInt, 0, 10);
            var evt = new JSONObject
            {
                ["_time"] = ev["b"],
                ["_type"] = type,
                ["_value"] = value
            };
            events.Add(evt);
        }
    }

    if (v3.HasKey("rotationEvents"))
    {
        foreach (JSONNode ev in v3["rotationEvents"].AsArray)
        {
            var type = ev["et"].IsNumber ? ev["et"] : ev["t"];
            var evt = new JSONObject
            {
                ["_time"] = ev["b"],
                ["_type"] = type,
                ["_value"] = ClampInt(ev["rot"].AsInt, -360, 360)
            };
            events.Add(evt);
        }
    }

    if (v3.HasKey("colorBoostBeatmapEvents"))
    {
        foreach (JSONNode ev in v3["colorBoostBeatmapEvents"].AsArray)
        {
            var type = ev["et"].IsNumber ? ev["et"] : ev["t"];
            var evt = new JSONObject
            {
                ["_time"] = ev["b"],
                ["_type"] = type,
                ["_value"] = ev["boost"].AsBool ? 1 : 0
            };
            events.Add(evt);
        }
    }

    v2["_events"] = events;

    if (v3.HasKey("customData"))
    {
        var converted = CustomDataObject(v3["customData"]);
        if (converted != null)
            v2["_customData"] = converted;
    }

    return v2;
}

    private static JSONObject ConvertNote(JSONObject v3Note)
    {
        if (v3Note == null) return null;

        var note = new JSONObject();

        if (v3Note.HasKey("b")) note["_time"] = v3Note["b"];
        if (v3Note.HasKey("x")) note["_lineIndex"] = v3Note["x"];
        if (v3Note.HasKey("y")) note["_lineLayer"] = v3Note["y"];
        if (v3Note.HasKey("c")) note["_type"] = v3Note["c"];
        if (v3Note.HasKey("d")) note["_cutDirection"] = v3Note["d"];

        // Do NOT include "a"
        // You can optionally remove unknown keys if needed

        return note;
    }

    private static JSONObject MakeSafeNote(JSONNode b, JSONNode x, JSONNode y, JSONNode c, JSONNode d)
    {
        return new JSONObject
        {
            ["_time"] = b,
            ["_lineIndex"] = ClampInt(x.AsInt, 0, 3),
            ["_lineLayer"] = ClampInt(y.AsInt, 0, 2),
            ["_type"] = ClampInt(c.AsInt, 0, 1),
            ["_cutDirection"] = ClampInt(d.AsInt, 0, 8)
        };
    }

    private static int ClampInt(int val, int min, int max)
    {
        if (val < min || val > max)
            Debug.LogWarning($"Clamping value: {val} → {Mathf.Clamp(val, min, max)}");
        return Mathf.Clamp(val, min, max);
    }

}
