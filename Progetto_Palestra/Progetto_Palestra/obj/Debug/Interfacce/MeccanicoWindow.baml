   M S B A M L   `   `   `  ����L  HProgetto_Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Progetto_PalestraProgetto_Palestra  5   	   2    ,Progetto_Palestra.Interfacce.MeccanicoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Progetto_Palestra     5   	     � Title$  MeccanicoWindow��5   	   $	��450��6!   $��813.573��6.   .��5	      � .+�5
   
   �� -   5         ��Name$ ButtonRipara��5
      $��Ripara Attrezzo��6'   $
��Left=�6A   $��600,111,0,0q�6\   $	��Top=�6q   $	��196��6�   ���	#FF0066FF���	#FF0066FF6�   �� 5   
   
�������$
��Left=�6!   $��37��6<   ���	#FF7D7C83$	��Top=�6[   $	��888��6s   $	��0.5��6   �� 5   
   $%��Meccanico / Elenco segnalazioni��6   $
��Left=�6:   $��10,7,0,0q�6U   $	��Top=�6g   $	��185��6   ���	#FF7D7C836�   �� 5   
   -   5         ��Name$ LabelBenvenuto��5      $��Benvenuto/a ��6(   $
��Left=�6?   $��	463,7,0,0q�6Z   $	��Top=�6m   $	��333��6�   ���	#FF7D7C836�   �� 5   
   -   5        $ ButtonTermina��5      $��Termina riparazione��6(   $
��Left=�6F   $��600,148,0,0q�6a   $	��Top=�6v   $	��196��6�   ���	#FF0066FF���	#FF0066FF6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DG_Segnalazioni��5         CanUserAddRows .     
IsReadOnly . $
��Left=�6U   $	��329��6p   $��	10,80,0,0q�6}   $	��Top=�6�   $	��585��6�   �� 5   
   -   5        $ ButtonDisconnetti��5      $��Disconnettiti��6,   $
��Left=�6D   $��600,377,0,0q�6_   $	��Top=�6t   $	��196��6�   ���	#FF0066FF���	#FF0066FF6�   �� 5   
   $&�� Elenco segnalazioni disponibili:��6   $
��Left=�6;   $��	10,51,0,0q�6V   $	��Top=�6i   $	��185��6�   ���	#FF7D7C836�   5      5      