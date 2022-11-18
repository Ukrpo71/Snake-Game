mergeInto(LibraryManager.library, {

  SaveExtern: function (data) {
    var dataString = UTF8ToString(data);
    var myObj = JSON.Parse(dataString);
    player.setData(myObj);
  },

  LoadExtern: function () {
    player.getData().then(_data=> {
      const myJSON = JSON.stringify(_data);
      myGameInstance.SendMessage('DataPersist', 'LoadYandexData',myJSON);
    })
  },

  GetMode: function () {
    var returnStr =  initPlayer().then(_player => {
        _player.getMode()});
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    console.log(buffer);
    return buffer;
  },

});