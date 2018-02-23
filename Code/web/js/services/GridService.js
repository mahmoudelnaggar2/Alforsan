angular.module('enozomApp').service('GridService', function () {
    getSortExpression = function (orderByParams, defaultOrderBy) {
        var OrderBy = defaultOrderBy;
        if (orderByParams && orderByParams.length > 0) {
            OrderBy = orderByParams[0].substring(1);
        }
        return OrderBy;
    },
    getSortDirection = function (orderByParams) {
        var OrderByDirection = 'ASC';
        if (orderByParams && orderByParams.length > 0) {
            OrderByDirection = orderByParams[0].charAt(0);

            if (OrderByDirection == '+')
                OrderByDirection = 'ASC';
            else
                OrderByDirection = 'DESC';
        }

        return OrderByDirection;
    },
    getRowIndex = function (index, pageSize, pageNumber) {
        return index + 1 + pageSize * (pageNumber - 1);
    }

    return {
        getSortExpression: getSortExpression,
        getSortDirection: getSortDirection,
        getRowIndex: getRowIndex

    };
});