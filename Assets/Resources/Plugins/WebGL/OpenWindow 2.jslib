var OpenWindowPlugin = {
    openWindow: function(link)
    {
      console.write("Calling OpenWindowPlugin for link: " + link);
    	var url = Pointer_stringify(link);
        document.onmouseup = function()
        {
        	window.open(url);
        	document.onmouseup = null;
        }
    }
};

mergeInto(LibraryManager.library, OpenWindowPlugin);
