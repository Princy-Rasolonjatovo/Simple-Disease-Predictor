   M S B A M L   `   `   `  ����K  GDiseasePredictor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DiseasePredictorDiseasePredictor  5   	   !    DiseasePredictor.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:DiseasePredictor     5   	     � Title$  Disease Predictor��5   	   $	��350��6#   $	��699��60    � 
ResizeMode$ NoResize=�5	   	   .��5      � -   5         �Name$ GridMainLayout��5      .+�5   
   �� -   5         ��Name$ SearchButton��5      $��Search��6'   $
��Left=�68   $��	10,41,0,0q�6S   $	��Top=�6f   $	��100��6~   $��29��6�   $��Segoe UI)�6�   $
��Bold"�6�   $��11(�6�   6�   �� 5   
   $��Disease Predictor ��6   $
��Left=�6-   $��	10,10,0,0q�6H   $	��Top=�6[    J� RenderTransformOrigin$ 0.535,0.464&�6s   $	��262��6�   �� 5   
   -   5         ��Name$ ComboSelector0��5      $
��Left=�6+   $��	10,94,0,0q�6F   $	��Top=�6Y   $	��210��6q   5      �� 5   
   -   5        $ 
AddSymptom��5      $��Add Symptoms��6%   $
��Left=�6<   $��
131,41,0,0q�6W   $	��Top=�6k   4  .System.Windows.Automation.AutomationProperties   HelpText$ Add a new symptom��6�      Name$ AddSymptomsButton��6�   $ -0.154,0.494&�6�   $	��141��6!  $��29��6-  �� 5   
   -   5        $ ComboSelector1��5      $
��Left=�6+   $��
237,94,0,0q�6F   $	��Top=�6Z   $	��210��6r   6~   �� 5   
   -   5        $ ComboSelector2��5      $
��Left=�6+   $��
464,94,0,0q�6F   $	��Top=�6Z   $	��210��6r   6~   �� 5   
   -   5        $ RemoveSymptom��5      $��Remove Symptom��6(   $
��Left=�6A   $��
306,41,0,0q�6\   $	��Top=�6p   $ Add a new symptom��6�   $ AddSymptomsButton��6�   $ -0.154,0.494&�6  $	��141��6)  $��29��65  � 5   
   -   5        $ Sub_GridMainLayout��5      5      5      