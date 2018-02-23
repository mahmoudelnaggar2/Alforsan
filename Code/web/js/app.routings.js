/* Setup Rounting For All Pages */
enozomApp.config([
    '$stateProvider', '$urlRouterProvider', '$httpProvider', 'Rights',

    function($stateProvider, $urlRouterProvider, $httpProvider, Rights) {

        $httpProvider.interceptors.push('sessionInjector');

        // Redirect any unmatched url
        $urlRouterProvider.otherwise("/pages/home");

        $stateProvider
            .state('login', {
                url: "/login",
                templateUrl: "views/global/login.html",
                data: { pageTitle: 'Admin Dashboard Template' },
                controller: 'LoginController',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/global/LoginController.js',
                                    'css/login.css'
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('pages', {
                url: '/pages',
                abstract: true,
                templateUrl: 'views/global/pages.html',
                controller: 'AppController'
            })
            .state('users', {
                url: "/users",
                templateUrl: "views/security/users.html",
                parent: 'pages',
                data: { pageTitle: 'Users List', right: Rights.PUBLIC },
                controller: "UsersListController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/security/UsersListController.js'
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('UserAdd', {
                url: "/user",
                templateUrl: "views/security/user.html",
                parent: 'pages',
                data: { pageTitle: 'Add User', right: Rights.PUBLIC },
                controller: "UserController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/security/UserController.js',
                                    'js/factories/security/UsersFactory.js'
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('UserEdit', {
                url: "/user/:id",
                templateUrl: "views/security/User.html",
                parent: 'pages',
                data: { pageTitle: 'Edit User', right: Rights.PUBLIC },
                controller: "UserController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/security/UserController.js',
                                    'js/factories/security/UsersFactory.js'

                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('roles', {
                url: "/roles",
                templateUrl: "views/security/roles.html",
                parent: 'pages',
                data: { pageTitle: 'Roles List', right: Rights.PUBLIC },
                controller: "RolesListController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/security/RolesListController.js'
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('RoleAdd', {
                url: "/role",
                templateUrl: "views/security/role.html",
                parent: 'pages',
                data: { pageTitle: 'Add New Role', right: Rights.PUBLIC },
                controller: "RoleController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/security/RoleController.js'
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('RoleEdit', {
                url: "/role/:id",
                templateUrl: "views/security/role.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Role', right: Rights.ROLES },
                controller: "RoleController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/security/RoleController.js',
                                    'js/factories/security/RolesFactory.js'
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('denied', {
                url: "/denied",
                templateUrl: "views/global/Denied.html",
                parent: 'pages',
                data: { pageTitle: 'Access Denied', right: Rights.PUBLIC },
                controller: "DeniedController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/global/DeniedController.js",
                                    'assets/admin/layout/css/themes/darkblue-rtl.css',
                                    'css/error.css'
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('profile', {
                url: "/profile",
                templateUrl: "views/global/profile.html",
                parent: 'pages',
                data: { pageTitle: 'Edit User Profile', right: Rights.PUBLIC },
                controller: "ProfileController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/global/ProfileController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('home', {
                url: "/home",
                templateUrl: "views/global/dashboard.html",
                parent: 'pages',
                data: { pageTitle: 'Dashboard', right: Rights.DASHBOARD },
                controller: "DashboardController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/global/DashboardController.js",
                                    'js/factories/admin/GradeFactory.js',
                                    'js/services/DownLoadService.js',
                                    'js/factories/admin/ReportFactory.js',
                                    "../assets/layouts/global/scripts/"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('dashboard', {
                url: "/dashboard",
                templateUrl: "views/global/dashboard.html",
                parent: 'pages',
                data: { pageTitle: 'Dashboard', right: Rights.DASHBOARD },
                controller: "DashboardController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/global/DashboardController.js",
                                    'js/factories/admin/GradeFactory.js',
                                    'js/services/DownLoadService.js',
                                    'js/factories/admin/ReportFactory.js',
                                    "../assets/layouts/global/scripts/"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Studentdashboard', {
                url: "/students/:statusId",
                templateUrl: "views/global/dashboard.html",
                parent: 'pages',
                data: { pageTitle: 'Dashboard', right: Rights.DASHBOARD },
                controller: "DashboardController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/global/DashboardController.js",
                                    'js/factories/admin/GradeFactory.js',
                                    "../assets/layouts/global/scripts/",
                                    'js/services/DownLoadService.js',
                                    'js/factories/admin/ReportFactory.js',

                                ]
                            }]);
                        }
                    ]
                }
            })

        /* Accounting pages routing */
        .state('new_applicant', {
            url: "/new_applicant",
            templateUrl: "views/accounting/pull_new_applicant.html",
            parent: 'pages',
            data: { pageTitle: 'Define Schools', right: Rights.PUBLIC },
            controller: "NewApplicantController",
            resolve: {
                deps: [
                    '$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'enozomApp',
                            files: [
                                "js/controllers/accounting/NewApplicantController.js",
                                "js/factories/admin/StudentsFactory.js",
                                'js/enums/StudentStatus.js',
                            ]
                        }]);
                    }
                ]
            }
        })
            /* End accounting pages routing */

        /* Admin pages routing */


        /* Admin pages routing */
        .state('schools', {
            url: "/schools",
            templateUrl: "views/admin/schools.html",
            parent: 'pages',
            data: { pageTitle: 'Define Schools', right: Rights.SCHOOLS },
            controller: "SchoolsController",
            resolve: {
                deps: [
                    '$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'enozomApp',
                            files: [
                                "js/controllers/admin/SchoolsController.js",
                                "js/factories/admin/SchoolsFactory.js",
                                "assets/global/plugins/bootbox/bootbox.min.js"
                            ]
                        }]);
                    }
                ]
            }
        })
            .state('Add School', {
                url: "/school_add",
                templateUrl: "views/admin/school_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Add School', right: Rights.SCHOOLS },
                controller: "ManageSchoolController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/admin/ManageSchoolController.js",
                                    "js/factories/admin/SchoolsFactory.js",
                                    "assets/global/plugins/bootbox/bootbox.min.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Edit School', {
                url: "/school_edit/:id",
                templateUrl: "views/admin/school_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit School', right: Rights.SCHOOLS },
                controller: "ManageSchoolController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/admin/ManageSchoolController.js",
                                    "js/factories/admin/SchoolsFactory.js",
                                    "assets/global/plugins/bootbox/bootbox.min.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('grades', {
                url: "/grades",
                templateUrl: "views/admin/grades.html",
                parent: 'pages',
                data: { pageTitle: 'Define Grades', right: Rights.GRADES },
                controller: "GradesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/GradeFactory.js",
                                    "js/controllers/admin/GradesController.js",
                                    "assets/global/plugins/bootbox/bootbox.min.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('add grade', {
                url: "/grade_add",
                templateUrl: "views/admin/grade_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Grade', right: Rights.GRADES },
                controller: "editGradesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/GradeFactory.js",
                                    "js/controllers/admin/ManageGradeController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('edit grade', {
                url: "/grade_edit/:id",
                templateUrl: "views/admin/grade_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Grade', right: Rights.GRADES },
                controller: "editGradesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/GradeFactory.js",
                                    "js/controllers/admin/ManageGradeController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('fees_types', {
                url: "/fees_types",
                templateUrl: "views/admin/fees_types.html",
                parent: 'pages',
                data: { pageTitle: 'Define Fees types', right: Rights.FEESTYPES },
                controller: "FeesTypesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/admin/FeesTypesController.js",
                                    "js/enums/feesType.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Add Fees Type', {
                url: "/fees_type_add",
                templateUrl: "views/admin/fees_type_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Add Fees Type', right: Rights.FEESTYPES },
                controller: "ManageFeesTypesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/admin/ManageFeesTypesController.js",
                                    "js/factories/admin/FeesTypesFactory.js",
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Edit Fees Type', {
                url: "/fees_type_edit/:id",
                templateUrl: "views/admin/fees_type_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Fees Type', right: Rights.FEESTYPES },
                controller: "ManageFeesTypesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/admin/ManageFeesTypesController.js",
                                    "js/factories/admin/FeesTypesFactory.js",
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('years', {
                url: "/years",
                templateUrl: "views/admin/years.html",
                parent: 'pages',
                data: { pageTitle: 'Define year', right: Rights.YEARS },
                controller: "YearsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/YearFactory.js",
                                    "js/controllers/admin/YearsController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Add Year', {
                url: "/year_add",
                templateUrl: "views/admin/year_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Year', right: Rights.PUBLIC },
                controller: "ManageYearsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/YearFactory.js",
                                    "js/controllers/admin/ManageYearController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Edit Year', {
                url: "/year_edit/:id",
                templateUrl: "views/admin/year_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Year', right: Rights.PUBLIC },
                controller: "ManageYearsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/YearFactory.js",
                                    "js/controllers/admin/ManageYearController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })


            //Nationality
               .state('Nationalities', {
                   url: "/Nationalities",
                   templateUrl: "views/admin/nationalities.html",
                   parent: 'pages',
                   data: { pageTitle: 'Define Nationality', right: Rights.PUBLIC },
                   controller: "NationalitiesController",
                   resolve: {
                       deps: [
                           '$ocLazyLoad',
                           function ($ocLazyLoad) {
                               return $ocLazyLoad.load([{
                                   name: 'enozomApp',
                                   files: [
                                       "js/factories/admin/NationalityFactory.js",
                                       "js/controllers/admin/NationalitiesController.js"
                                   ]
                               }]);
                           }
                       ]
                   }
               })
            .state('Add Nationality', {
                url: "/nationality_add",
                templateUrl: "views/admin/nationality_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Add Nationality', right: Rights.PUBLIC },
                controller: "ManageNationalityController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/NationalityFactory.js",
                                    "js/controllers/admin/ManageNationalityController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Edit Nationality', {
                url: "/nationality_edit/:id",
                templateUrl: "views/admin/nationality_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Nationality', right: Rights.PUBLIC },
                controller: "ManageNationalityController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/NationalityFactory.js",
                                    "js/controllers/admin/ManageNationalityController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })

            //end nationality
        .state('discounts_types', {
            url: "/discounts_types",
            templateUrl: "views/admin/discounts_types.html",
            parent: 'pages',
            data: { pageTitle: 'Define Discounts types', right: Rights.PUBLIC },
            controller: "DiscountsTypesController",
            resolve: {
                deps: [
                    '$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'enozomApp',
                            files: [

                                "js/controllers/admin/DiscountsTypesController.js"
                            ]
                        }]);
                    }
                ]
            }
        })
            .state('Add Discounts Type', {
                url: "/discounts_type_add",
                templateUrl: "views/admin/discounts_type_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Discounts types', right: Rights.PUBLIC },
                controller: "ManageDiscountsTypesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/DicountsTypesFactory.js",
                                    "js/controllers/admin/ManageDiscountsTypesController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('Edit Discounts Type', {
                url: "/discounts_type_edit/:id",
                templateUrl: "views/admin/discounts_type_edit.html",
                parent: 'pages',
                data: { pageTitle: 'Edit Discounts types', right: Rights.PUBLIC },
                controller: "ManageDiscountsTypesController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/DicountsTypesFactory.js",
                                    "js/controllers/admin/ManageDiscountsTypesController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })


        /* End admin pages routing */
        .state('grade_fees', {
            url: "/grade_fees",
            templateUrl: "views/admin/grade_fees.html",
            parent: 'pages',
            data: { pageTitle: 'Define Fees for grades', right: Rights.GRADEFEES },
            controller: "GradeFeesController",
            resolve: {
                deps: [
                    '$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'enozomApp',
                            files: [
                                "js/factories/admin/GradeFeesFactory.js",
                                'js/factories/admin/GradeFactory.js',
                                "js/controllers/admin/GradeFeesController.js"
                            ]
                        }]);
                    }
                ]
            }
        })
            /* End admin pages routing */
            /* applicant pages routing */
            .state('AdmissionApp', {
                url: "/AdmissionApp/:id",
                templateUrl: "views/applicant/admission_application/admission_application.html",
                parent: 'pages',
                data: { pageTitle: 'Admission Application', right: Rights.ADMISSIONAPPLICATION },
                controller: "AdmissionApplicationController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/applicant/AdmissionApplicationController.js",
                                    "js/factories/admin/StudentsFactory.js",
                                    'js/factories/admin/GradeFactory.js',
                                    'js/factories/admin/ReportFactory.js',
                                    'js/services/DownLoadService.js',
                                    'js/controllers/global/CalenderController.js',
                                    'js/enums/ParentStatus.js',
                                    'js/enums/InterviewStatus.js',
                                    'js/enums/StudentStatus.js',
                                    'js/enums/Custody.js',
                                    'js/enums/Religion.js',
                                    'js/enums/Nationality.js',
                                ]
                            }]);
                        }
                    ]
                }

            }).state('transferToStudent', {
                url: "/transferToStudent",
                templateUrl: "views/applicant/transferToStudent_admission.html",
                parent: 'pages',
                data: { pageTitle: 'Transfer To Student', right: Rights.TRANSFERTOSTUDENT },
                controller: "TransferToStudentController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/GradeFactory.js",
                                    "js/factories/admin/StudentFactory.js",
                                    "js/controllers/applicant/TransferToStudentController.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            /* End applicant pages routing */
            /* start student affairs pages routing */
            .state('upgradeStudents', {
                url: "/upgradeStudents",
                templateUrl: "views/studentAffairs/upgrade_students.html",
                parent: 'pages',
                data: { pageTitle: 'Upgrade Student', right: Rights.UPGRADESTUDENT },
                controller: "UpgradeStudentsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/StudentAffairs/StudentFinancialFactory.js",
                                    "js/controllers/studentAffairs/UpgradeStudentsController.js",
                                    "js/factories/admin/StudentsFactory.js"
                                ]
                            }]);
                        }
                    ]
                }
            }).state('issueVoucher', {
                url: "/issueVoucher",
                templateUrl: "views/studentAffairs/issue_voucher.html",
                parent: 'pages',
                data: { pageTitle: 'Issue Voucher', right: Rights.ISSUEVOUCHER },
                controller: "IssueVoucherController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/enums/VoucherTypeEnum.js",
                                    "js/factories/admin/GradeFeesFactory.js",
                                    "js/factories/admin/YearFactory.js",
                                    "js/factories/StudentAffairs/StudentVoucherFactory.js",
                                    "js/factories/StudentAffairs/StudentFinancialFactory.js",
                                    "js/factories/admin/studentFactory.js",
                                    'js/controllers/global/CalenderController.js',
                                    "js/controllers/studentAffairs/IssueVoucherController.js",
                                     "js/services/DownLoadService.js",
                                    "js/factories/admin/ReportFactory.js",
                                    "js/enums/FeesType.js"
                                ]
                            }]);
                        }
                    ]
                }
            })
            .state('returnIssueVoucher', {
                url: "/issueVoucher/:id",
                templateUrl: "views/studentAffairs/issue_voucher.html",
                parent: 'pages',
                data: { pageTitle: 'Issue Voucher', right: Rights.ISSUEVOUCHER },
                controller: "IssueVoucherController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/enums/VoucherTypeEnum.js",
                                    "js/factories/admin/GradeFeesFactory.js",
                                    "js/factories/admin/YearFactory.js",
                                    "js/factories/admin/studentFactory.js",
                                    "js/factories/StudentAffairs/StudentVoucherFactory.js",
                                    "js/factories/StudentAffairs/StudentFinancialFactory.js",
                                    "js/controllers/studentAffairs/IssueVoucherController.js",
                                       "js/services/DownLoadService.js",
                                    "js/factories/admin/ReportFactory.js",
                                    "js/enums/FeesType.js"
                                ]
                            }]);
                        }
                    ]
                }

            })
            .state('setting', {
                url: "/setting",
                templateUrl: "views/admin/setting.html",
                parent: 'pages',
                data: { pageTitle: 'Settingr', right: Rights.SETTING },
                controller: "settingController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/admin/SettingController.js"
                                ]
                            }]);
                        }
                    ]
                }

            })
            .state('searchForStudent', {
                url: "/searchForStudent",
                templateUrl: "views/studentAffairs/search_for_student.html",
                parent: 'pages',
                data: { pageTitle: 'Search For Student', right: Rights.PUBLIC },
                controller: "SearchForStudentController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/factories/admin/GradeFactory.js",
                                    "js/factories/admin/StudentFactory.js",
                                    "js/controllers/studentAffairs/SearchForStudentController.js",
                                    "js/enums/StudentStatus.js"

                                ]
                            }]);
                        }
                    ]
                }

            })
            /* End student affairs pages routing */
            /* start Reports pages routing */
            .state('interviewReports', {
                url: "/interviewReports",
                templateUrl: "views/Reports/InterviewReports.html",
                parent: 'pages',
                data: { pageTitle: 'Interview Reports', right: Rights.INTERVIEWREPORTS },
                controller: "InterviewReportsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/services/CalenderService.js',
                                    'js/services/DownLoadService.js',
                                    'js/factories/admin/ReportFactory.js',
                                    'js/factories/admin/StudentsFactory.js',
                                    'js/controllers/Reports/InterviewReports.js'
                                ]
                            }]);
                        }
                    ]
                }

            })
            .state('admissionReports', {
                url: "/admissionReports",
                templateUrl: "views/Reports/AdmissionReports.html",
                parent: 'pages',
                data: { pageTitle: 'Admission Reports', right: Rights.ADMISSIONREPORTS },
                controller: "admissionReportsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/services/CalenderService.js',
                                    'js/services/DownLoadService.js',
                                    'js/factories/admin/ReportFactory.js',
                                    'js/factories/admin/StudentsFactory.js',
                                    'js/controllers/Reports/AdmissionReports.js'
                                ]
                            }]);
                        }
                    ]
                }

            })
            .state('voucherfullReports', {
                url: "/voucherfullReports",
                templateUrl: "views/Reports/VoucherFullReports.html",
                parent: 'pages',
                data: { pageTitle: 'Voucher Full Reports', right: Rights.VOUCHERFULLREPORTS },
                controller: "voucherFullReportsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    "js/controllers/Reports/VoucherFullReports.js",
                                       'js/services/DownLoadService.js',
                                    'js/factories/admin/ReportFactory.js'
                                ]
                            }]);
                        }
                    ]
                }

            })
            .state('voucherprintoutReports', {
                url: "/voucherprintoutReports",
                templateUrl: "views/Reports/VoucherPrintOutReports.html",
                parent: 'pages',
                data: { pageTitle: 'Voucher print Out Reports', right: Rights.VOUCHERPRINTOUTREPORTS },
                controller: "voucherPrintOutReportsController",
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load([{
                                name: 'enozomApp',
                                files: [
                                    'js/controllers/Reports/VoucherPrintOutReports.js',
                                       'js/services/DownLoadService.js',
                                'js/factories/admin/ReportFactory.js'
                                ]
                            }]);
                        }
                    ]
                }

            })
         .state('PaidFeesReport', {
             url: "/PaidFeesReport",
             templateUrl: "views/Reports/PaidFeesReport.html",
             parent: 'pages',
             data: { pageTitle: 'Paid Fees Report', right: Rights.INTERVIEWREPORTS },
             controller: "PaidFeesReportController",
             resolve: {
                 deps: [
                     '$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load([{
                             name: 'enozomApp',
                             files: [
                                 'js/services/CalenderService.js',
                                 'js/services/DownLoadService.js',
                                 'js/factories/admin/ReportFactory.js',
                                 "js/controllers/Reports/PaidFeesReport.js"
                             ]
                         }]);
                     }
                 ]
             }

         })

        .state('PreviousSchoolsReports', {
            url: "/PreviousSchoolsReports",
            templateUrl: "views/Reports/PreviousSchoolsReports.html",
            parent: 'pages',
            data: { pageTitle: 'Previous Schools Reports', right: Rights.INTERVIEWREPORTS },
            controller: "PreviousSchoolsReportsController",
            resolve: {
                deps: [
                    '$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'enozomApp',
                            files: [
                                'js/services/DownLoadService.js',
                                'js/factories/admin/ReportFactory.js',
                                'js/factories/admin/StudentsFactory.js',
                                'js/controllers/Reports/PreviousSchoolsReports.js'
                            ]
                        }]);
                    }
                ]
            }

        })
         .state('UnPaidFeesReport', {
             url: "/UnPaidFeesReport",
             templateUrl: "views/Reports/UnPaidFeesReport.html",
             parent: 'pages',
             data: { pageTitle: 'UnPaid Fees Report', right: Rights.INTERVIEWREPORTS },
             controller: "UnPaidFeesReportController",
             resolve: {
                 deps: [
                     '$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load([{
                             name: 'enozomApp',
                             files: [
                                 'js/services/DownLoadService.js',
                                 'js/factories/admin/ReportFactory.js',
                                 'js/controllers/Reports/UnPaidFeesReport.js'
                             ]
                         }]);
                     }
                 ]
             }

         })

          .state('NewApplicantsReport', {
              url: "/NewApplicantsReport",
              templateUrl: "views/Reports/NewApplicantsReport.html",
              parent: 'pages',
              data: { pageTitle: 'New Applicants Report', right: Rights.PUBLIC },
              controller: "NewApplicantsReportController",
              resolve: {
                  deps: [
                      '$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load([{
                              name: 'enozomApp',
                              files: [
                                  'js/services/DownLoadService.js',
                                  'js/factories/admin/ReportFactory.js',
                                  'js/factories/admin/StudentsFactory.js',
                                  'js/controllers/Reports/NewApplicantsReport.js'
                              ]
                          }]);
                      }
                  ]
              }

          })
        /* End Reports pages routing */

         //Inter View Results
         .state('InterViews', {
             url: "/InterViews",
             templateUrl: "views/applicant/interview_result/interviews.html",
             parent: 'pages',
             data: { pageTitle: 'InterView Appointments', right: Rights.PUBLIC },
             controller: "InterViewsController",
             resolve: {
                 deps: [
                     '$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load([{
                             name: 'enozomApp',
                             files: [
                                  "js/factories/admin/StudentFactory.js",
                                    'js/factories/admin/GradeFactory.js',
                                    "../assets/layouts/global/scripts/",
                                    'js/services/DownLoadService.js',
                                    'js/factories/admin/ReportFactory.js',
                                    "js/controllers/applicant/InterViewsController.js",

                             ]
                         }]);
                     }
                 ]
             }
         })
         //Inter View Status
         .state('SetInterViewStatus', {
             url: "/SetInterViewStatus/:id",
             templateUrl: "views/applicant/interview_result/setInterViewStatus.html",
             parent: 'pages',
             data: { pageTitle: 'InterView Status', right: Rights.InterViewStatus },
             controller: "InterViewStatusController",
             resolve: {
                 deps: [
                     '$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load([{
                             name: 'enozomApp',
                             files: [
                                     "js/controllers/applicant/InterViewStatusController.js",
                                     "js/factories/admin/StudentFactory.js",
                                    'js/factories/admin/GradeFactory.js',
                                    'js/factories/admin/ReportFactory.js',
                                    'js/services/DownLoadService.js',
                                    'js/controllers/global/CalenderController.js',
                                    'js/enums/ParentStatus.js',
                                    'js/enums/InterviewStatus.js',
                                    'js/enums/StudentStatus.js',
                                    'js/enums/Custody.js',
                                    'js/enums/Religion.js',
                                    'js/enums/Nationality.js',
                             ]
                         }]);
                     }
                 ]
             }

         });
       
    }
]);