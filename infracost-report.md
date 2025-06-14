Project: UPT-FAING-EPIS/proyecto-si784-2025-i-u2-scae-upt/WebApp/pyWeb_ScaeUPT/terraform/plan.json

 Name                                  Monthly Qty  Unit                  Monthly Cost   
                                                                                         
 azurerm_container_registry.main                                                         
 ├─ Registry usage (Basic)                      30  days                         $5.00   
 ├─ Storage (over 10GB)           Monthly cost depends on usage: $0.10 per GB            
 └─ Build vCPU                    Monthly cost depends on usage: $0.0001 per seconds     
                                                                                         
 azurerm_service_plan.main                                                               
 └─ Instance usage (F1)                        730  hours                        $0.00   
                                                                                         
 OVERALL TOTAL                                                                  $5.00 

*Usage costs were estimated using infracost-usage.yml, see docs for other options.

──────────────────────────────────
4 cloud resources were detected:
∙ 2 were estimated
∙ 2 were free

┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━┳━━━━━━━━━━━━┓
┃ Project                                                          ┃ Baseline cost ┃ Usage cost* ┃ Total cost ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━╋━━━━━━━━━━━━┫
┃ UPT-FAING-EPIS/proyecto-si784-2...eb_ScaeUPT/terraform/plan.json ┃            $5 ┃       $0.00 ┃         $5 ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━┻━━━━━━━━━━━━┛
## 💾 Costos adicionales manuales

| 🏷️ Recurso | 💵 Costo mensual |
|-------------|-------------------|
| Base de Datos MySQL en Elestika | **$2.78** |

> **Nota:** Estos son costos adicionales no rastreados por Terraform pero incluidos en el presupuesto total de infraestructura.
