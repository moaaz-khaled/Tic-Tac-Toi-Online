mergeInto(LibraryManager.library, {
  WebGLClipboard_Arm: function (textPtr) {
    var text = UTF8ToString(textPtr);

    if (!window.__unityClip) {
      window.__unityClip = { pending: null };

      var fallbackCopy = function (t) {
        var ok = false;
        var ta = document.createElement('textarea');
        ta.value = t;
        ta.style.position = 'fixed';
        ta.style.opacity = '0';
        document.body.appendChild(ta);
        ta.focus();
        ta.select();
        try { ok = document.execCommand('copy'); } catch (e) { ok = false; }
        document.body.removeChild(ta);
        var canvas = document.querySelector('canvas');
        if (canvas) canvas.focus();
        return ok;
      };

      var doCopy = function () {
        var t = window.__unityClip.pending;
        if (t === null) return;
        window.__unityClip.pending = null;

        if (!fallbackCopy(t) && navigator.clipboard && navigator.clipboard.writeText) {
          navigator.clipboard.writeText(t).catch(function (e) {
            console.warn('Clipboard copy failed', e);
          });
        }
      };

      ['pointerup', 'mouseup', 'touchend', 'click', 'keyup'].forEach(function (ev) {
        document.addEventListener(ev, doCopy, true);
      });
    }

    window.__unityClip.pending = text;
  }
});