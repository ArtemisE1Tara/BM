   M S B A M L   `   `   `  ����G  CBMBF Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BMBF_ManagerBMBF_Manager  5   	       BMBF_Manager.Mods   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:BMBF_Manager     5   	   -   5          � 
ResizeMode$  CanMinimize=�5   	    � WindowStyle$
 None=�5	   	    � Title$! BMBF Manager Mod Installing��5   	   $	��374��6-   $	��660��6:   ���	#FF282828.��5      � .+�5   
   �� $��Logo.png��6   $
��Left=�6"   $��25��6=   $	��Top=�6I   $��25��6a   $��10,2,0,0q�6l   �� 5   
   -   5        $��X��5   G   $
��Left=�6S   $��	616,4,0,0q�6n   $	��Top=�6�   $��31��6�   ���	#00000000���	#FFCBCBCB6�   �� 5   
   -   5        $��|��5   F   $
��Left=�6R   $��
583,-2,0,0q�6m   $	��Top=�6�   $��20��6�   ���	#00000000���	#FFCBCBCB J� RenderTransformOrigin$ 0.5,0.5&�6�   $��31��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 90Q�6&   T� 5      5      5      �� 5   
   -   5         ��Name$ Quest��5   �   $��lucida console)�6+    J� Opacity$	 0.8Q�6�   $
��Left=�6�   $��23��6�   $��
203,30,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $��Quest IP��6�   $	��Top=�6   $	��445��6  ���	#FF494949���	#FFD30000
�������6S  �� 5   
   -   5        $ txtbox��5   v   $��lucida console)�6:   $	 0.8Q�6V    �� 
IsReadOnly . $
��Left=�6�   $	��333��6�   $��	10,30,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��188��6�   ���	#FF494949���	#FFA6A6A6���	#FFD300006'  �� 5   
   $	 0.8Q�6   $
��Left=�6#   $��24��6>   $	��Top=�6J   $	��249��6b   $ 0.5,0.5&�6n   $#��BMBF Manager - Mod Installing��6�   	 �� FontSize$	 14(�6�   
 �� 
Foreground

 �   �$��40,4,0,0q�6�   �� 5   
   -   5        $��lucida console)�5   9   $	 0.8Q�6U   $��Install selected Mod��6v   $
��Left=�6�   $��417,330,0,0q�6�   $	��Top=�6�   $	��230��6�   $��33��6�   $ -0.858,0.891&�6�   ���	#FF005654���	#FFF3F3F3���	#FFB947476H  �� 5   
   -   5         ��Name$ ModList��5   W   $��lucida console)�6;   $
��Left=�6h   $	��267��6�   $��
203,58,0,0q�6�   $	 0.8Q�6�   $	��Top=�6�   $	��445��6�   ���#0065a3���#171717���	#FFB94747 �� View 5      �� 5       .P�5!      ��  �� Width$	 160��6%    �� DisplayMemberBinding 61   �� *Name+��5"      �� 5#      
 � Tag$
 Name��63   .��6>   
Mod Name6H   5%      �� 5&      $	 120��6%    61   �� *	Creator+��5'      �� 5(      $ Creator��63   .��6A   	Creator6J   5*      �� 5+      $ 60��6%    60   �� *
ModVersion+��5,      �� 5-      $ 
ModVersion��63   .��6D   	Version6M   5/      �� 50      $	 100��6%    61   �� *GameVersion+��51      �� 52      $ GameVersion��63   .��6E   Game Version6S   54      55      57      �� 58   
   -   5        $��lucida console)�58   9   $	 0.8Q�6U   $��	More Info��6t   $
��Left=�6�   $��205,330,0,0q�6�   $	��Top=�6�   $	��207��6�   $��33��6�   $ -0.858,0.891&�6�   ���	#FF005654���	#FFF3F3F3���	#FFB947476;  5:      5;      