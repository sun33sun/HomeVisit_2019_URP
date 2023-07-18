var ImageDownloaderPlugin = {
    ImageDownloader: function (str, fn) {
        console.log("start jslib download");
        var msg = Pointer_stringify(str);
        var fname = Pointer_stringify(fn);
        var contentType = 'image/jpeg';

        function fixBinary(bin) {
            var length = bin.length;
            var buf = new ArrayBuffer(length);
            var arr = new Uint8Array(buf);
            for (var i = 0; i < length; i++) {
                arr[i] = bin.charCodeAt(i);
            }
            return buf;
        }
        //atob解码使用base64编码的字符串
        var binary = fixBinary(atob(msg));
        var data = new Blob([binary], { type: contentType });
        //创建一个html dom用于触发blob下载
        var link = document.createElement('a');
        link.download = fname;
        link.innerHTML = 'DownloadFile';
        link.setAttribute('id', 'ImageDownloaderLink');
        link.href = window.URL.createObjectURL(data);
        link.onclick = function () {
            var child = document.getElementById('ImageDownloaderLink');
            child.parentNode.removeChild(child);
        };
        link.style.display = 'none';
        document.body.appendChild(link);
        link.click();
        window.URL.revokeObjectURL(link.href);
    }
};
//并入Unity中，官方写法。
mergeInto(LibraryManager.library, ImageDownloaderPlugin)