Instructions:

1 - Copy the persistant scene from the package folder and paste it to your project folder.
2 - include it to the build settings (first position)
3 - include all other scenes you want to load
4 - in the Manager prefab in persistant scene, find the PhaseManager, rename the phases accordingly to the scenes you wan to load.
You can also set the inputs there to trigger each scene indivudualy using the keyboard for instance
5 - Add a "gameObject" to each scene and include the script: Send
6 - Enjoy!


to add this package in package manage:
- add new scopedRegisteries in ProjectSettings/Package manager
- name: jeanf
- url: https://registry.npmjs.com
- scope jfr.art