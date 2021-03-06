=== Advanced Inspector ===
by LightStriker Software

The Inspector, or in other engine the "property grid", is one of the most important tool we have to use. 
It's the window to all our data, and if this tool is lacking, everything we do and how fast we do it, will also suffer from it.

There's lot of Inspector package in the Asset Store, but none that we feel give us the "full package". 
To solve this issue, we've been working on this package for over seven months. One of the first thing we found out is, 
the classes behind the Inspector - SerializedProperty, SerializedObject - were also too limited for our needs, 
surprisingly restrained to a single serialization context. We rewrote them from scratch.

In this package, everything how the Inspector draw and handle data have been reworked from the ground up. 
"Advanced Inspector" is what we believe Unity's Inspector should have been from the start.

Our DLL also comes with a fully commented .xml so our comments shows up in your code editor.
Please read the AdvancedInspector_Manual.pdf for more information about the inner working of each parts.

Video of the AdvancedInspector in action; https://www.youtube.com/watch?v=jvctw4HndKY
Video of the ActionBinding in action; https://www.youtube.com/watch?v=OUFMgCBoCao
Manual online; http://lightstrikersoftware.com/docs/AdvancedInspector_Manual.pdf

The list of features supported in this package is too long for a short readme. Please refer to the manual.


== Bugs, Issues, Support ==

You can contact us directly and we will answer as quickly as possible.

Email : admin@lightstrikersoftware.com
Website : www.lightstrikersoftware.com

You can also contact us on Unity's forum, user "LightStriker".


== FAQ ==

- Why a DLL?

It's easier to distribute, but mostly there's some internal code that are not fully debugged outside the scope of the Advanced Inspector. 
For example, we have a TreeView that is not completed, but for the AI - listing non-nested items - it works.

- Can I get access to the source?

Contact us, maybe we can deal something, but no promises.
The package, as is, should perform any task you want it to perform without the need to the source.
The Advanced Inspector is fully expandable.

- Mac? Linux?

We didn't test this on Linux, but we did on Mac. 

- I found a bug!

Contact us right away! We will fix it ASAP.

- I got idea for other features...

Again, contact us! We will look into adding that feature - if humanly possible - as quickly as we can.

- I don't understand how it works, how to use it or how to implement it!

Contact us, we will gladly help you.


== Version ==

1.0:
- Release

1.1:
- Fixed the Camera preview (the window can be dragged around)
- Fixed the Camera perspective/orthographic drop down with new RestrictedAttribute option.
- Added SkinnedMeshRenderer and Text Mesh as converted editor.
- Added a way to decouple data from description in the RestrictedAttribute. Just pass DescriptorPair instead of the object directly.
- Added a missing constructor in ReadOnlyAttribute.
- Changed the constraint field in the RigidBody because "I prefer the checkboxes".
- Non-editable expandable field - such as object deriving from System.Object - now display info in the format "ToString() [Class Name]". Overriding ToString becomes useful. 
- Fixed a null error in the Restricted > Toolbox.
- SizeAttribute can now flag a collection as not-sortable. Useful for collection which the order comes from the code and should not be changed.
- Added a new Attribute; RuntimeResolve which display a field editor based on the object's type or a type returned by a delegate, instead of the field type. 
It can also be used to restrict a Object field to a specific type. For example, you can make a UnityEngine.Object field only display Material.
- Added a new Attribute; CollectionConstructor which let you create a collection item with the constructor and parameter of your choice.
- Moved the asset into Plugins and Plugins/Editor folder, so other language - like JS - can be supported without modification.
- Copied the Examples asset into JavaScript, also used to test other language support.
- Added a class named "ActionBinding" which is a way to bind an event to a data-driven method invokation. Similar to what Unity will roll out in 4.6 with their new GUI... but better.
Take a look at the video for more information. It's an example of what the AI can do, as no editor were writing for that class.

1.2:
- Fixed an issue with Editor object showing the log message about no reference towards them when saving a scene. It was harmless, but annoying.
- Fixed an issue with internal texture showing the same log message.
- Fixed a index exception while deleting an item in some specific cases. It was harmless, but annoying.
- Fixed the multi-edition on boolean editor.
- Update some internal icon; +, -, open, closed and the drag icon. They should be easier to see in both light and dark.
- Added IPreview interface that gives control over the preview panel of the inspector. New class in the DLL; InspectorPreview.
- Added a new attribute; "Tab". Draws toolbar-like tabs at the top of a script, similar to the SkinnedCloth component.
- Added the standard "on scene" collider editor on the Box, Capsule, and Sphere collider.
- Added a class named TypeUtility. It is used only to retrieve a type in all loaded assembly based on its name.
- Added SkinnedMeshRenderer, TextMesh and Mesh editor.
- Added "very large array" support. Any collection having over 50 items only displays 50, and have arrows at the top and bottom to navigate in them.
- Added UDictionary, a fully Unity-serializable dictionary for Unity 4.5 and later. Does not work on previous Unity version.
- Added RangeInt/RangeFloat, struct used to set a minimum and a maximum. They use Unity's MinMaxSlider.
- The Space attribute can now add space before an item and/or after it. Before default size is 0, after default size is 1. Does not change existing behaviour.

1.3:
- IMPORTANT: All classes related to the Advanced Inspector have been moved to the AdvancedInspector namespace, for consistency with other assemblies (Ex.: System.Serializable) and not to pollute the global scope.
- The AdvancedInspector Attribute now have "InspectDefaultItem" property, which emulate the way Unity display items without the need to add [Inspect] on all your items. You can still inspect item Unity would not display by adding the Inspect Attribute.
- Help Attribute can now be tagged on a class, giving a help box in the header or footer. You can also place multiple instance of the Help attributes, which displays multiple help box.
- Help Attribute now has the "Position" property, which place the help box before or after the targeted member, or in case of a class, in the header or footer.
- Expandable Attribute now has the "Expanded" property, which forces an item to be expanded by default when first viewed.
- Group Attribute now has the "Expandable"  property, which when false, the group is not collapsable and remains open, serving has a dividing box.
- Added a layout "header" and "footer" at the top and bottom of the inspector, before and after all the fields.
- Added an interface; IInspectorRunning, which gives access to the Header and Footer zone in the form of a OnHeaderGUI and OnFooterGUI method.
- InspectorField now support SerializableProperties, but please do not use it, it's most likely full of bug since SerializedProperty is anything but generic.
- CTRL + Click expand all the child of the item expanded. (Ex.: All the subobject in a list)
- Removed the "[class name]" part of the label of a list when that item is the same type as the list itself. (Ex.: List<item>, no need to tell you it's an item)
- The collection type is now only display in Advanced or Debug mode.
- Removed the "shadows" from the following icon; add, delete, folder open, folder close.
- Fixed a null exception thrown when using enums in a Dictionary.
- Added cubemap to the supported type in the Preview.
- Reworked the layout a lot, such as adding 4 pixel to the left of the fields so they don't stick to the window. Now nested item are "boxed-in", which should make multi-level items easier to read.
- Lined up lot of items and subitem that were not properly lined up in specific case in both their label and fields.
- Fixed the Example6 where nested class where not serialized.
- Added methods "GetAttributes" and "GetAttributes<T>" to the Inspector field, returning all instance of a multi-attributes.
- Fixed the aspect ratio in the Camera preview on Scene.
- SkinnedMeshRender "Bones" property is no longer "read only", but it is now an advanced property.
- Added LightEditor to support Unity's Lights.
- Added support for the Gradient type, another of those fancy hidden thing in Unity.
- The cursor is now a "grab hand" when hovering over the drag control.
- Added an EditorWindow example of external inspector.

1.31:
- Added a type-check when displaying an object label to see if the type overload ToString, so it would be called only if a proper implementation exist, and not the base one.
- Inspector Level and Sorting are now saved in EditorPrefs.
- Expansion state are now saved in EditorPrefs. 
- Dragging a label to another label automatically performs a copy-paste of that field.

1.32:
- Fixed undo issue with restricted field.
- Fixed undo issue with struct such as Vector, Quaternion, Bound, Rect, RectOffset.
- Fixed undo issue with string.
- Added some shading to the expandable boxes.
- Added the GameObject/Component picker tool.

1.4:
- Added a new attribute; Background, which color the box of expandable item.
- MaskedEnum attribute is replaced by the Enum attribute, which also controls how an enum is displayed.
- Merge the Size attribute with the CollectionConstructor attribute into the new Collection attribute. 
- Toolbars are no longer using the toolbar style by default. You can do row of buttons this way.
- Toolbars are now drawn on the header, because they collided with the separator.
- Descriptor color now colors field instead of label. It made no sense to color the label, and was often hard to read.
- Descriptor with only a color no longer pass on an empty name to the label.
- Added a missing constructor in AdvancedInspector attribute.
- Expandable attribute now has the InspectDefaultItems similar to the AdvancedInspector attribute.
- Fixed an error raised when encountering a type with multiple overload of ToString.
- Fixed an issue when undoing creation or deletion of an item in a collection was not properly refreshing it.
- Fixed an issue that prevented proper undoing of collection reordering. 
- Added multiple display option to the Enum attribute, see the EnumDisplay enum.
- Added multiple display option to the Collection attribute, see the CollectionDisplay enum.
- Added multiple display option to the Restrict attribute, see the RestrictDisplay enum.
- Added a "Collection Locked" option to the contextual menu, it locks every collection from adding/removing/sorting items.
- Added a Tutorial documentation, which gives steps by steps examples of implementation.
- Fixed an issue that prevented copy pasting AnimationCurve.
- Fixed an issue that custom-made Editor that were not overloading RefreshFields would fail.
- Removed the "EditedTypes" and "EditDerived" from the InspectorEditor, as the CustomEditor attribute values are now used.

1.41:
- Fixed a Null Exception on the CameraEditor on Unity 4.6+.
- Fixed a Array initialization issue introduced with the changes to the Collection attribute.
- Fixed a ComponentMonoBehaviour destruction issue. Even while not referenced, the instance would fail to be destroyed.
- Fixed a collection failing to raise the Erase event on ComponentMonoBehaviour.
- Fixed a issue when a Dictionary would contain ComponentMonoBehaviour and would destroy them even when it shouldn't.
- Fixed a 4 pixels layout issue when multiple nested object would be part of a parent collection.
- Fixed an issue where multiple nested instance of the same type would prevent the child node from being expandable.

1.42:
- Added a new interface; "ICopy" which give an object the power to handle how it should be copied over a targeted field. 
- Added a new interface; "ICopiable" which allows an object to decide if it can be copied or not over a targeted field.
- The Inspector now takes the .NET ICloneable interface into account, allowing it to handle the copying. ICopy take priority over ICloneable.
- Fixed a recursive stack overflow in the copy/paste of a self-referenced object.
- Fixed a stack overflow in prefab comparaison with self-referenced object in editor mode.
- Added a "Take Screenshot" option in the Camera editor. It's available in advanced mode.
- The Collection attribute was missing the IRuntimeAttribute interface declaration.
- ActionBinding now properly sorts out properties, ignoring Getter when a Set is needed and vise versa.
- ActionBinging now flags Binding Parameter that are extra - not declared in the constructor - as being external or static, never internal.
- ActionBinging now control if it can be copied over, and what is copied. Same thing for BindingParameters. See ICopy/ICopiable.
- Fixed an issue when reloading the assembly context where Unity would "hang" for a few second while the Inspector rebuilds the type hierarchy tree.
- Added a missing construction in the Space Attribute.
- Fixed the stack overflow in the ComponentMonoBehaviour... again! What was I drinking?
- Fixed a object array initilization issue when using Collection(0) and a inlined field initialization.
- AnimatorEditor no longer display items twice.
- Added to the AdvancedInspector namespace the following; Toolbox, ModalWindow, WindowResult, IModal.
- The modal window are now draggable. 
- Fixed the expansion of collection in Button mode.

1.43:
- Fixed an expansion bug with groups.
- Fixed an expansion bug with multi-edition of collection and dictionary.
- Fixed an issue that would prevent ExternalEditor list from being sortable.
- Added the compile define ADVANCED_INSPECTOR to detect if the tool is installed or not.
- SHIFT+DRAG on labels of integer or float to change the value.
- DOUBLE+CLICK on labels expends or collapsed the item if it's expandable. 
- DescriptorAttribute is now taken into account when afixed to an enum's value. 
- The CollectionAttribute now has a "Enum Type" properties, which binds a collection to an enum's names. See the documentation for an example.
- Fixed an invalid index when a small array is turned bigger using the CollectionAttribute size property. 
- Added missing attribute targets in a few Attribute so they could be added to structs. 
- New Attribute; MethodAttribute, which gives control on how a method is invoked or displayed. For example, you can replace the botton and draw whatever you want.
- ExternalEditor got more control over how it can be drawn; fixed separator or no space reserved for expander.
- The separator in the ExternalEditor is now uncoupled from the separator in the Inspector. Previously they shared the same settings.
- Transform's global position/orientation are not read only anymore, and accessible in Advanced mode instead of Debug.
- Expandable attribute can now be placed on Interfaces. See AIExample_Interface for an example of an implementation. 
- CollectionDisplay "default" value have been renamed to "List", to better reflect what it does.
- UnityEngine.Component that implement an interface can now be properly drag and dropped in a field of that interface type. See AIExample_Interface.

1.44:
- Made "SelectedTab" property public in InspectorField.
- Fixed an issue when mixing groups with tabs.
- Optimized the lookup of FieldEditor attribute. If you had slow down with this attribute, it should be fine now.
- Now able to inspect static field, properties and methods. Static fields and properties are read only, except when the game is playing.
- InspectorField now has a Static property. 
- Added tooltips and URL to scripting documentation for the Camera, Animator, Light, and many other editors.
- Added editors for Animation, RigidBody2D, SpringJoint2D, Sprite Renderer, and many other classes.
- Note that editors for 2D Physic (Ex.: HingeJoint2D) requires Unity 4.5 or higher.
- InspectorField now supports SerializedProperty arrays. Try to not break that feature.
- Fixed an issue with IDataChanged when the item inspected is at the root.
- Exception thrown on a method/delegate invokation now returns the inner exception, which would make debugging your own method a lot easier.
- AdvancedInspector attribute are now properly inherited in classes hierarchy.
- Enum drop down now properly uses Descriptor for their names.
- Removed the compile define, it didn't work from within a library.
- Fixed an issue that in some case an enum dropdown would become uneditable.
- Nicify enum variables.
- Fixed an issue where class help attribute would fail to invoke their referenced methods properly. 
- Fixed an issue that made enum returns an odd value when they were explicitly flagged another. (Ex.: -315) 
- Fixed Restrict attribute when applied to a collection, so it's used by the child field instead of the parent.
- Fixed an issue where private member of a parent class would become invisible in the derived class.

1.5:
- Now support Unity 5.
- No longer support Unity 4.3, only 4.5+.
- Added the Watch window, which allows you to track inspector values. Right click any value and select "Watch".
- Added the Selected History. Use CTRL+Left Arrow and CTRL+Right Arrow to cycle in your previous selection.
- Added the Runtime Save feature; it allows you to save changed value while playing the game. This feature is in beta and may have issues.
- Redone all the examples, should be way easier to use.
- Added a collection of supported Unity type, like Terrain Collider and Clothes. 
- Due to confusion and overlapping functionnality, the Expandable attribute is now limited to fields and properties and only override the expandability of an item.
- Fixed an issue where sortable list would fail.
- Fixed an issue of recursivity with self-referencing objects.
- Fixed an issue with RuntimeResolved attribute in non-dynamic mode.
- Fixed the draggable icon in Unity Pro.
- Minor tweak to the expandable boxes visual; should look smoother.
- Minor changes in multi-selection.


Tested on; 4.5.0 / 4.6.0