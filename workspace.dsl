workspace "Care Leavers" "A high level architectural overview of the planned Care Leavers website" {

    model {
        properties {
            "structurizr.groupSeparator" "/"
        }

        group "DfE" {

            careLeavers = softwareSystem "Care Leavers Website" {
                webapp = container "Care Leavers Website" {
                    description "Information and guidance website for care leavers and those approaching leaving care"
                    technology "dotnet 8.0"
                    tags "Microsoft Azure - Website Staging"
                }

                tags "Microsoft Azure - Website Staging"
            }

        }

        group "Contentful" {

            contentful = softwareSystem "Contentful" {
                // technology "Content Management System"
                tags "Microsoft Azure - Blob Page"

                contentfulCore = container "Contentful Core" {
                    technology "Content Management System"
                    tags "Microsoft Azure - Blob Page"
                    description "Cloud hosted content management system storing data and providing an admin system to edit content"

                    group "Content Models" {
                        components = group "Components" {
                            navigationElement = component "Navigation Element"
                            configuration = component "Configuration" {
                                -> navigationElement "Links to"
                            }

                            card = component "Card"
                            externalAgency = component "External Agency"
                            callToAction = component "Call To Action"
                            grid = component "Grid" {
                                -> card "May Contain"
                                -> externalAgency "May Contain"
                                -> callToAction "May contain"
                            }
                            definition = component "Definition"
                            definitionLink = component "Definition Link" {
                                -> definition "Links to"
                            }
                            banner = component "Banner"
                            richContent = component "Rich Content"
                            richContentBlock = component "Rich Content Block" {
                                -> richContent "Contains"
                            }
                            migrationTracker = component "Migration Tracker"

                        }

                        riddleGroup = group "Riddle" {
                            answer = component "Answer"
                            statusChecker = component "Status Checker" {
                                -> answer "Contains"
                            }
                            riddle = component "Riddle"
                        }

                        builtIn = group "Built-In" {
                            image = component "Image (Asset)" {
                                banner -> this "May contain"
                                card -> this "May contain"
                                externalAgency -> this "May contain"
                            }

                            richText = component "Rich Text Area" {
                                -> grid "May contain"
                                -> callToAction "May contain"
                                -> image "May contain"
                                -> definition "May contain"
                                -> definitionLink "May contain"
                                -> banner "May contain"
                                -> richContentBlock "May contain"
                                -> statusChecker "Links to"
                                -> riddle "Links to"
                            }

                            definition -> richText
                        }


                        pageTemplates = group "Page Templates" {
                            page = component "Page"

                            !elements "pageTemplates->" {
                                this -> richText "Contains"
                            }

                            definition -> page "Links to"
                            navigationElement -> page "Links to (Navigation)"
                            configuration -> page "Links to (Homepage)"
                            answer -> page "Links to"

                        }



                    }
                }

                this -> careLeavers "Receives content from" "HTTPS"




            }
        }

        group "Azure" {
            translate = softwareSystem "Microsoft Translate" {
                tags "Microsoft Azure - Translator Text"

                translateAPI = container "Translation API" {
                    technology "Microsoft AI"
                    tags "Microsoft Azure - Translator Text"
                    description "LLM powered translation service"

                    this -> webapp "Receives translations from" "HTTPS"
                }

            }

        }


        live = deploymentEnvironment "Live" {

            azure = deploymentNode "Microsoft Azure" {
                tags "Microsoft Azure - Azure Deployment Environments"

                region = deploymentNode "West Europe" {
                    tags "Microsoft Azure - Region Management"

                    translateInstance = containerInstance translateApi {

                    }

                    dns = infrastructureNode "DNS Router" {
                        technology "Azure DNS"
                        description "Routes incoming requests based upon domain name"
                        tags "Microsoft Azure - DNS Zones"
                    }
                    gateway = infrastructureNode "Azure Front Door" {
                        technology "Azure Front Door and Firewalls"
                        description "Automatically distributes and secures incoming application traffic"
                        tags "Microsoft Azure - Firewalls"
                        dns -> this "Forwards requests to" "HTTPS"
                    }

                    web = deploymentNode "Autoscaling Web" {
                        tags "Microsoft Azure - Auto Scale"

                        deploymentNode "App Service Plan" {
                            tags "Microsoft Azure - App Service Plans"

                            deploymentNode "App Service" {
                                tags "Microsoft Azure - App Services"
                                webApplicationInstance = containerInstance webapp {
                                    gateway -> this "Fowards requests to" "HTTPS"


                                }
                            }
                        }
                    }

                    cache = deploymentNode "Autoscaling Cache" {
                        tags "Microsoft Azure - Auto Scale"
                        redis = infrastructureNode "Distributed Cache" {
                            technology "Redis"
                            tags "Microsoft Azure - Cache Redis	"

                            webApplicationInstance -> this "Writes data to"
                            this -> webApplicationInstance "Reads data from"
                        }
                    }

                }
            }


            contentfulDeployment = deploymentNode "Contentful" {




                cdn = infrastructureNode "Contentful CDN" {
                    tags "Microsoft Azure - API Management Services"
                    -> webApplicationInstance "Reads data from" "HTTPS"
                }

                webhooks = infrastructureNode "Contentful Web Hooks" {
                    tags "Microsoft Azure - Web Jobs"
                    -> dns "Sends update notifications to" "HTTPS"
                }

                core = containerInstance contentfulCore {
                    -> cdn "Reads data from"
                    -> webhooks "Receives data from"
                }

            }






        }
    }

    views {
        systemLandscape {
            autoLayout tb
            include *

        }

        deployment * live {
            autolayout lr
            include *
        }

        component contentfulCore {
            autolayout tb
            include *
        }

        themes https://static.structurizr.com/themes/microsoft-azure-2023.01.24/theme.json
    }
}