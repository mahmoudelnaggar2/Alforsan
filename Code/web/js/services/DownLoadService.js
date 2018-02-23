angular.module('enozomApp').service('DownLoadService', function() {
    downLoadPdf = function(fileData, fileName) {
        var file = new Blob([fileData], {
            type: 'application/pdf'
        });
        var downloadLink = angular.element('<a></a>');
        downloadLink.attr('href', window.URL.createObjectURL(file));
        downloadLink.attr('download', fileName + '.pdf');
        downloadLink[0].click();
    },
    downLoadExcel = function (fileData, fileName) {
        var file = new Blob([fileData], {
            type: 'application//vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        });
        var downloadLink = angular.element('<a></a>');
        downloadLink.attr('href', window.URL.createObjectURL(file));
        downloadLink.attr('download', fileName + '.xls');
        downloadLink[0].click();
    }
    return {
        downLoadPdf: downLoadPdf,
        downLoadExcel: downLoadExcel
    };


});