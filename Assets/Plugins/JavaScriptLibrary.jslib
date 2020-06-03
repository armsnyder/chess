mergeInto(LibraryManager.library, {

    OpenURL: function (url) {
        window.location = Pointer_stringify(url);
    },

    GetQueryString: function () {
        var str = window.location.search;
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    }
});
