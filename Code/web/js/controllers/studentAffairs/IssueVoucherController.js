angular.module('enozomApp').controller('IssueVoucherController', ['$rootScope', '$state', '$scope', '$translate', '$filter', '$window', '$modal', '$stateParams',
    'CRUDFactory', 'gradeFeeFactory', 'yearFactory', '$q', 'voucherTypeEnum', 'settings', 'studentFinancialFactory', 'GridService', 'NgTableParams', 'studentFactory', 'FeesType', 'ReportFactory', 'DownLoadService',
    function($rootScope, $state, $scope, $translate, $filter, $window, $modal, $stateParams, CRUDFactory, gradeFeeFactory, yearFactory, $q,
        voucherTypeEnum, settings, studentFinancialFactory, GridService, NgTableParams, studentFactory, FeesType, ReportFactory, DownLoadService) {

        ////////////////////////////////////////////////////Variable ////////////////////////////////////////////////////////////////
        $scope.student = "";
        $scope.feeTypes = "";
        $scope.filterObject = { SearchObject: {} };
        $scope.filterObject_Refund = { SearchObject: {} };
        $scope.studentVoucher = { PaymentDate: new Date(), TotalVoucher: 0, StudentVoucherDetails: [] };
        $scope.studentRefund = { PaymentDate: new Date(), VoucherRefundDate: new Date() };
        $scope.balance = 0;
        $scope.selectFeeList = [];
        $scope.selectFeeMessage = false; //selectFeeMessage
        $scope.submitted = false;
        $scope.FeesType = FeesType;
        $scope.GenerateBtn = false;
        $scope.SelectedVoucher = {};


        ///////////////////////////////////////////////////End Variable//////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////General Function/////////////////////////////////////////////////////////
        if ($stateParams.id) {
            $scope.yearPromise = CRUDFactory.getList("Year").then((data) => {
                $scope.years = data.data;
                var currentYear = $scope.years.filter(function (item) {
                    return item.IsCurrent == true;
                })[0];
                $scope.studentVoucher.YearId = currentYear.YearId;
                CRUDFactory.get('Students', $stateParams.id).then((data) => {
                    $scope.student = data.data;
                    $scope.FeeStudentVoucherValidation();
                    $scope.voucherListFun();

                }, (error) => { console.log("error at promise", error); });
            });
              
            
        };
        $scope.FeeStudentVoucherValidation = function () {
            var studyYearId = $scope.studentVoucher.YearId;
         
            gradeFeeFactory.FeeForStudentvoucher($stateParams.id, $scope.student.GradeId, studyYearId).then((data) => {
                console.log("FeeTypes", data.data)
                $scope.feeTypes = data.data;
                $scope.selectFeeList = [];
            }, (error) => { console.log("feetype error"); });
        };

        $scope.voucherListFun = function() {
            var initialParams = {
                count: settings.pageSize
            };

            //voucher table
            var initialSettings = {
                counts: [],
                paginationMaxBlocks: 13,
                paginationMinBlocks: 2,
                getData: function(params) {
                    var orderBy = params.orderBy();
                    var OrderByParam = GridService.getSortExpression(orderBy, 'VoucherDate');
                    $scope.filterObject.PageNumber = params.page();
                    $scope.filterObject.SortBy = OrderByParam;
                    $scope.filterObject.SortDirection = "DESC";
                    $scope.filterObject.PageSize = settings.pageSize;
                    $scope.filterObject.SearchObject.StudentId = $scope.student.StudentId;
                    return CRUDFactory.getPaginatedList("StudentVoucher", $scope.filterObject).then((data2) => {
                        $scope.getBalanceFun();
                        params.total(data2.data.TotalRecords); console.log(data2.data)
                        $scope.isPaid = data2.data.Results[0].IsPaid
                        return $scope.voucherList = data2.data.Results;
                    }, (error) => {});
                }
            };
            $scope.vouchertable = new NgTableParams(initialParams, initialSettings);
            //refund table
            var initialSettings_Refund = {
                counts: [],
                paginationMaxBlocks: 13,
                paginationMinBlocks: 2,
                getData: function(params) {
                    var orderBy = params.orderBy();
                    var OrderByParam = GridService.getSortExpression(orderBy, 'VoucherRefundDate');
                    $scope.filterObject_Refund.PageNumber = params.page();
                    $scope.filterObject_Refund.SortBy = OrderByParam;
                    $scope.filterObject_Refund.SortDirection = "DESC";
                    $scope.filterObject_Refund.PageSize = settings.pageSize;
                    $scope.filterObject_Refund.SearchObject.StudentId = $scope.student.StudentId;
                    return CRUDFactory.getPaginatedList("StudentVoucherRefund", $scope.filterObject_Refund).then((data_Refund) => {
                        $scope.getBalanceFun();
                        params.total(data_Refund.data.TotalRecords);
                        return $scope.refundList = data_Refund.data.Results;
                    }, (error) => {});
                }
            };
            $scope.refundtable = new NgTableParams(initialParams, initialSettings_Refund);
        };

        $scope.OpenPopupWindow = function() {
            $state.go('searchForStudent');
        };

        $scope.getBalanceFun = function() {
            studentFinancialFactory.getBalance($scope.student.StudentId).then((data) => {
                $scope.balance = data.data;
                // $scope.UserBalanceMessage();
            }, (error) => {})
        };

        $scope.calculateTotal = (event, fee, feeTypeId, allowDiscount, discountPercentage) => { 
            $scope.stateOfCheckBox = event.target.checked;
            var feeListObject = {};
            feeListObject.FeesTypeId = feeTypeId;
            feeListObject.Fee = fee;
            var amount = 0;
            if (allowDiscount == true && discountPercentage!=null) {
                amount = fee - (fee * (discountPercentage / 100));
            }
            else
                amount = fee;
            if ($scope.stateOfCheckBox) {
                $scope.studentVoucher.TotalVoucher += amount;
                $scope.studentVoucher.StudentVoucherDetails.push(feeListObject);
            } else {
                $scope.studentVoucher.TotalVoucher -= amount;
                $scope.studentVoucher.StudentVoucherDetails = $scope.studentVoucher.StudentVoucherDetails.filter(function(item) {
                    return item.FeesTypeId != feeTypeId;
                });
            }
            $scope.checkCheckBox();
        };

        $scope.checkCheckBox = function() {
            var count = 0;
            if ($scope.selectFeeList.length == 0) {
                $scope.selectFeeMessage = true;
            }
            $scope.selectFeeList.forEach(function(check) {
                if (check) {
                    count += 1;
                }
            });
            if (count > 0) {
                $scope.selectFeeMessage = false;
            } else {
                $scope.selectFeeMessage = true;
            }
        };

        $scope.resetCheckBox = function() {
            angular.forEach($scope.feeTypes, function(feeType) {
                $scope.selectFeeList[feeType.FeesTypeId] = false;
            });
            $scope.studentVoucher.TotalVoucher = 0;
        };

        $scope.generateVoucher = function () {
            $scope.GenerateBtn = true;
            $scope.checkCheckBox();
            if ($scope.balance == 0 && !$scope.selectFeeMessage && $scope.studentVoucher.YearId>0) {
                $scope.submitted = true;
                $scope.studentVoucher.StudentId = $scope.student.StudentId;
                CRUDFactory.add('StudentVoucher', $scope.studentVoucher).then((data1) => {
                    $scope.submitted = false;
                    $scope.GenerateBtn = false;
                    $scope.getBalanceFun();
                    $scope.voucherListFun();
                    $scope.FeeStudentVoucherValidation();
                    $scope.resetCheckBox();
                }, (error) => { console.log("error create student voucher"); });
            }
        };
        $scope.resetFun = function() {
            $scope.student = "";
            $scope.feeTypes = "";
            $scope.filterObject = {};
            $scope.filterObject.SearchObject = {};
            $scope.filterObject_Refund = {};
            $scope.filterObject_Refund.SearchObject = {};
            $scope.studentVoucher = {};
            $scope.studentVoucher.TotalVoucher = 0;
            $scope.studentVoucher.StudentVoucherDetails = [];
            $scope.voucherList = [];
            $scope.refundList = [];
            $scope.selectFeeList = [];
            $scope.balance = 0;
            $scope.selectFeeMessage = false; //selectFeeMessage
        };

        $scope.hasError = function(field, validation) {
            if (validation) {
                return (($scope.voucherForm[field].$dirty &&
                        $scope.voucherForm[field].$error[validation] &&
                        !$scope.voucherForm[field].$pristine) ||
                    ($scope.edit && $scope.voucherForm[field].$error[validation]));
            }
            return ($scope.voucherForm[field].$dirty &&
                    $scope.voucherForm[field].$invalid &&
                    !$scope.voucherForm[field].$pristine) ||
                ($scope.edit && $scope.voucherForm[field].$invalid);
        };
        $scope.hasAddError = function (field, validation) {
            if (validation) {
                return (($scope.addForm[field].$dirty &&
                        $scope.addForm[field].$error[validation] &&
                        !$scope.addForm[field].$pristine) ||
                    ($scope.edit && $scope.addForm[field].$error[validation]));
            }
            return ($scope.addForm[field].$dirty &&
                    $scope.addForm[field].$invalid &&
                    !$scope.addForm[field].$pristine) ||
                ($scope.edit && $scope.addForm[field].$invalid);
        };
        ///////////////////////////////////////////////////End General Function/////////////////////////////////////////////////////
        ///////////////////////////////////////////////////Student Voucher//////////////////////////////////////////////////////////
        $scope.pay = function(voucherId) {
            $scope.paySubmit = function() {
                $scope.submitted = true;
                $scope.studentVoucher.StudentVoucherId = voucherId;
                CRUDFactory.edit('StudentVoucher', $scope.studentVoucher, voucherId).then((data) => {
                    $scope.voucherListFun();
                    $scope.resetCheckBox();
                    $scope.submitted = false;
                    $scope.GenerateBtn = false;
                    $('#payModel').modal('hide');
                }, (error) => {});
            };
        };
        $scope.displayPaymentNotes = function (vouchre) {
            $scope.SelectedVoucher.Notes = vouchre.Notes;
            $scope.SelectedVoucher.PaymentDate = vouchre.PaymentDate;
            $("#paymentSummary").modal('show');
        }
        $scope.deleteVoucher = function(voucher) {
            bootbox.confirm("Are you sure you want to delete this voucher?",
                function(result) {
                    if (result) {
                        CRUDFactory.delete('StudentVoucher', voucher.StudentVoucherId).then((data) => {
                                $scope.submitted = false;
                                $scope.getBalanceFun();
                                $scope.voucherListFun();
                            },
                            (error) => {});
                    }
                });
        };
        $scope.downloadReport = function (SVID) {
            $scope.promise1 = ReportFactory.VoucherReport(SVID);
            $q.all([$scope.promise1]).then((data) => {
                $scope.dataFile = data[0].data;
                DownLoadService.downLoadPdf($scope.dataFile, data[0].headers().filename);
            }, (error) => { console.log("error at promise", error) });
        };
        //////////////////////////////////////////////////End student voucher///////////////////////////////////////////////////////
        //////////////////////////////////////////////////Student Refund///////////////////////////////////////////////////////////
        $scope.refund = function(voucherId, totalVoucher) {
            $scope.edit = false;
            $scope.validRefundValue = false;
            $scope.submitted = false;
            $scope.isValidRefund = function() {
                if ($scope.studentRefund.TotalRefund > totalVoucher) {
                    return true;
                } else {
                    return false;
                }
            };

            $scope.refundSubmit = function(isValid) {
                $scope.submitted = true;
                $scope.edit = true;
                if (isValid && !$scope.validRefundValue) {
                    $scope.studentRefund.StudentVoucherId = voucherId;
                    $scope.studentRefund.StudentId = $scope.student.StudentId;
                    CRUDFactory.add('StudentVoucherRefund', $scope.studentRefund).then((data) => {
                            $scope.voucherListFun();
                            $scope.resetCheckBox();
                            $scope.submitted = false;
                            $('#refundModel').modal('hide');
                        },
                        (error) => {});
                }
            };
        };

        $scope.delivery = function(refundId) {
            $scope.submitted = false;
            $scope.deliveryRefund = {};
            $scope.deliverySubmit = function() {
                $scope.submitted = true;
                $scope.studentRefund.StudentVoucherRefundId = refundId;
                CRUDFactory.edit('StudentVoucherRefund', $scope.studentRefund, refundId).then((data) => {
                    $scope.voucherListFun();
                    $scope.resetCheckBox();
                    $('#deliveryRefundModel').modal('hide');
                    $scope.submitted = false;
                }, (error) => {});
            };
        };

        /////////////////////////////////////////////////End Student Refund/////////////////////////////////////////////////////////

        //////////////// search by student code//////////
        $scope.keypress = function(event) {
            if (event.keyCode == 13) {
                studentFactory.StudentByCode($scope.student.StudentCode).then((data) => {
                    $scope.student = data.data
                    $stateParams.id = $scope.student.StudentId
                    $scope.FeeStudentVoucherValidation();
                    $scope.voucherListFun();
                })
            }
        };
    }
]);